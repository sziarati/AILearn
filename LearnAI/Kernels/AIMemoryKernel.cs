using LearnAI.Interfaces;
using Microsoft.KernelMemory;

namespace LearnAI.Kernels
{
    public class AIMemoryKernel : IOpenAI
    {
        private IDictionary<string, string> _env;
        protected IKernelMemory kernelMemory { get; set; }
        private string _docName;
        public AIMemoryKernel(IDictionary<string, string> env, string docName)
        {
            _env = env;
            _docName = docName;
        }
        public async Task<IOpenAI> initialize()
        {
            //Create KM
            kernelMemory = new KernelMemoryBuilder()
                .WithOpenAIDefaults(_env["Key"].Trim())
                .Build<MemoryServerless>();

            //Feed KM with resource
            await kernelMemory.ImportDocumentAsync(_docName, documentId: "doc001");

            //Check if document is ready
            if (await kernelMemory.IsDocumentReadyAsync("doc001"))
            {
                return this;
            }
            return this;
        }

        public async Task<string> AskAsync(string question)
        {
            var answerResult = await kernelMemory.AskAsync(question);
            var answer = answerResult.Result;

            return answer;
        }
    }
}