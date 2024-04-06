using LearnAI;
using dotenv.net;

public class App{
    public async Task Run(string[] args)
    {

        DotEnv.Load();
        var env = DotEnv.Read();
        var openAI = new AIMemoryKernel(env);
        var result = await openAI.initialize();
        //var openAI = await new AIKernel(env).initialize();
        //if (!result)
        //{

        //    Console.WriteLine("document is not ready.");
        //    Console.ReadLine();
        //    return;
        //}

        Console.WriteLine("document is not ready. you can start asking questions.");
        while (true)
        {
            var question = Console.ReadLine();
            var answer = await openAI.AskAsync(question);
            Console.WriteLine(answer);
        }
    }
}