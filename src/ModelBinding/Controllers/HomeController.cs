using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelBinding.Models;
using ModelBinding.Models.Interfaces;

namespace ModelBinding.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() {
            var survey = new SurveyVM();
            var section = new SectionVM() { Label = "Section1" };
            section.Controls.Add(new QuestionVM() { Label = "Question1" });
            section.Controls.Add(new QuestionVM() { Label = "Question2" });
            section.Controls.Add(new QuestionVM() { Label = "Question3" });
            section.Controls.Add(new ButtonVM() { Label = "Button1" });
            section.Controls.Add(new QuestionVM() { Label = "Question4" });
            section.Controls.Add(new QuestionVM() { Label = "Question5" });
            section.Controls.Add(new LabelVM() { Label = "I am a label!" });
            survey.Controls.Add(section);

            var section2 = new SectionVM() { Label = "Section2" };
            section2.Controls.Add(new QuestionVM() { Label = "Question6" });
            section2.Controls.Add(new QuestionVM() { Label = "Question7" });
            section2.Controls.Add(new QuestionVM() { Label = "Question8" });
            section.Controls.Add(section2);

            return View(survey);
        }

        [HttpPost]
        public IActionResult Index(SurveyVM survey) {
            var id = survey.Id;
            return View(survey);
        }

        public class MyModelBinderProvider : IModelBinderProvider
        {
            public IModelBinder GetBinder(ModelBinderProviderContext context) {
                if (context == null) {
                    throw new ArgumentNullException(nameof(context));
                }

                if (context.Metadata.ModelType != typeof(IControl))
                {
                    return null;
                }

                var binders = new Dictionary<string, IModelBinder>();
                foreach (var type in typeof(MyModelBinderProvider).GetTypeInfo().Assembly.GetTypes()) {
                    var typeInfo = type.GetTypeInfo();
                    if (typeInfo.IsAbstract || typeInfo.IsNested) {
                        continue;
                    }

                    if (!(typeInfo.IsClass && typeInfo.IsPublic)) {
                        continue;
                    }

                    if (!typeof(IControl).IsAssignableFrom(type))
                    {
                        continue;
                    }

                    var metadata = context.MetadataProvider.GetMetadataForType(type);
                    var binder = context.CreateBinder(metadata);
                    binders.Add(type.FullName, binder);
                }

                return new MessageModelBinder(context.MetadataProvider, binders);
            }
        }

        public class MessageModelBinder : IModelBinder
        {
            private readonly IModelMetadataProvider _metadataProvider;
            private readonly Dictionary<string, IModelBinder> _binders;

            public MessageModelBinder(
                IModelMetadataProvider metadataProvider,
                Dictionary<string, IModelBinder> binders) {
                _metadataProvider = metadataProvider;
                _binders = binders;
            }

            public async Task BindModelAsync(ModelBindingContext bindingContext)
            {
                var controlTypeModelName = ModelNames.CreatePropertyModelName(bindingContext.ModelName, "ControlType");
                var controlTypeResult = bindingContext.ValueProvider.GetValue(controlTypeModelName);
                if (controlTypeResult == ValueProviderResult.None) {
                    bindingContext.Result = ModelBindingResult.Failed();
                    return;
                }

                IModelBinder binder;
                if (!_binders.TryGetValue(controlTypeResult.FirstValue, out binder))
                {
                    bindingContext.Result = ModelBindingResult.Failed();
                    return;
                }

                // Now know the type exists in the assembly.
                var type = Type.GetType(controlTypeResult.FirstValue);
                var metadata = _metadataProvider.GetMetadataForType(type);

                ModelBindingResult result;
                using (bindingContext.EnterNestedScope(
                    metadata,
                    bindingContext.FieldName,
                    bindingContext.ModelName,
                    model: null))
                {
                    await binder.BindModelAsync(bindingContext);
                    result = bindingContext.Result;
                }

                bindingContext.Result = result;
            }
        }
    }
}
