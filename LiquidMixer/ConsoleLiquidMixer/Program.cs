

using LiquidMixerApp;
using LiquidMixerApp.Inventory;
using LiquidMixerApp.Liquids;
using LiquidMixerApp.Mixer;
using LiquidMixerApp.SpeedStrategy;
using LiquidMixerApp.Timer;

Console.WriteLine("Start");
var mixer = new BasicMixer();
var speedGenerator = new IncrementalSpeedGenerator(0,100,1000);
var inventory = new LocalInventory();
var userInteractor = new ConsoleUserInteractor();
var logger = new LocalLogger();
var timeHandler = new TimeHandler();
var liquidA = new Liquid("liquid A", 100);

var liquidB = new Liquid("liquid B", 10);

var liquidC = new Liquid("liquid A", 200);
var addLiquidTask = inventory.AddAsync(liquidA, liquidB);
Task.WaitAll(addLiquidTask);




var liquidMixer = new LiquidMixer(mixer,inventory,speedGenerator, userInteractor, logger, timeHandler);


var startTask = liquidMixer.StartAsync(10000,  liquidC );

Console.WriteLine("Do something else");

Task.WaitAll(startTask);

Console.WriteLine("Finish");