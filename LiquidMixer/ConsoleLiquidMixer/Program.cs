

using LiquidMixerApp;
using LiquidMixerApp.Inventory;
using LiquidMixerApp.Liquids;
using LiquidMixerApp.Mixer;
using LiquidMixerApp.SpeedStrategy;
using LiquidMixerApp.Timer;

Console.WriteLine("Start");
var mixer = new BasicMixer();
var speedGenerator = new IncrementalSpeedGenerator(0,100,1000);
var inventory = new LocalLiquidInventory();
var userInteractor = new ConsoleUserInteractor();
var logger = new LocalLogger();
var timeHandler = new TimeHandler();
var liquidA = new Liquid("liquid A", 100);

var liquidB = new Liquid("liquid B", 10);

var liquidC = new Liquid("liquid A", 200);
var addLiquidTask = inventory.AddAsync(liquidA, liquidB);
Task.WaitAll(addLiquidTask);




var liquidMixer = new LiquidMixer(mixer,inventory,speedGenerator, timeHandler, logger);


var startTask = liquidMixer.StartAsync(10000,  liquidA, liquidB );


while (true)
{
    Thread.Sleep(1000);
    Console.WriteLine("Do something else");
    if (startTask.IsCompletedSuccessfully) break;
}

Task.WaitAll(startTask);

Console.WriteLine("Finish");