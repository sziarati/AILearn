using LearnAI.Interfaces;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace LearnAI.Kernels
{
    public class AIKernel : IOpenAI
    {
        private IDictionary<string, string> _env;
        private Kernel Kernel { get; set; }
        public AIKernel(IDictionary<string, string> env)
        {
            _env = env;
        }
        public async Task<IOpenAI> initialize()
        {
            var builder = Kernel.CreateBuilder();
            Kernel = builder.AddOpenAIFiles(_env["Key"].ToString())
                    .Build();      // Azure OpenAI Key

            return this;

        }

        public async Task<string> AskAsync(string question)
        {
            var prompt = @"{{$question}} One line TLDR with the fewest words.";

            var summarize = Kernel.CreateFunctionFromPrompt(prompt, executionSettings: new OpenAIPromptExecutionSettings { MaxTokens = 100 });

            string text1 = @"1st Law of Thermodynamics - Energy cannot be created or destroyed.
                             2nd Law of Thermodynamics - For a spontaneous process, the entropy of the universe increases.
                             3rd Law of Thermodynamics - A perfect crystal at zero Kelvin has zero entropy.";

            string text2 = @"1. An object at rest remains at rest, and an object in motion remains in motion at constant speed and in a straight line unless acted on by an unbalanced force.
                             2. The acceleration of an object depends on the mass of the object and the amount of force applied.
                             3. Whenever one object exerts a force on another object, the second object exerts an equal and opposite on the first.";

            Console.WriteLine(await Kernel.InvokeAsync(summarize, new() { ["input"] = text1 }));

            Console.WriteLine(await Kernel.InvokeAsync(summarize, new() { ["input"] = text2 }));

            return text1;
        }
    }
}
