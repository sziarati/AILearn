using dotenv.net;
using LearnAI.Kernels;

try
{
    var docName = "";
    var env = DotEnv.Read();
    while (true)
    {
        Console.WriteLine($"which document do you want to query:\n");
        Console.WriteLine($"Mabani Web.pdf (press 1) \n");
        var docNumber = Console.ReadLine();
        Console.WriteLine($"Loading document to query: \n");
        if (docNumber == "1")
            docName = @"Resources\Mabani Web.pdf";
        if (docName == "")
        {
            Console.WriteLine($"you did not select any document. \n");
            continue;
        }

        var openAI = new AIMemoryKernel(env, docName);

        //Feed KM with resource
        var result = await openAI.initialize();
        if (result != null)
        {
            Console.WriteLine("document is ready.\n");

            //Check if document is ready
            while (true)
            {
                var question = Console.ReadLine();
                var answer = await openAI.AskAsync(question);
                Console.WriteLine("----------------------------------------------------------------------");
                Console.WriteLine($"{answer}");
                Console.WriteLine("----------------------------------------------------------------------");
            }
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

/*
using IHost host = CreateHostBuilder(args).Build();
using var scope = host.Services.CreateScope();

var services = scope.ServiceProvider;

try
{
    services.GetRequiredService<App>().Run(args);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

IHostBuilder CreateHostBuilder(string[] strings)
{
    return Host.CreateDefaultBuilder()
        .ConfigureServices((_, services) =>
        {
            services.AddSingleton<App>();
            services.AddScoped<IOpenAI, AIMemoryKernel>();
        });
}*/

