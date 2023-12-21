using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec20;

public class PulseModules
{
    static readonly Dictionary<string, Module> Modules = new();
    static long _lowPulseCounter;
    static long _highPulseCounter;
    static long _buttonCounter;
    private static readonly Dictionary<string, long> Cycles = new(); 
    private static bool _pleaseStop;
    
    public PulseModules(List<List<string>> input)
    {
        Reset();
        
        foreach (var module in input)
        {
            switch (module[0])
            {
                case "broadcaster":
                    Modules.Add(module[0], new BroadCaster(module[1]));
                    break;
                default:
                    var op = module[0][0];
                    if (op == '%')
                    {
                        var name = module[0].Replace("%", "");
                        Modules.Add(name, new FlipFlop(module[1], name));
                        break;
                    }

                    if (op == '&')
                    {
                        var name = module[0].Replace("&", "");
                        Modules.Add(name, new Conjunction(module[1], name));
                        break;
                    }

                    throw new Exception("Unknown module type");
            }
        }

        var nonExistingDestinations = new List<string>();
        foreach (var module in Modules)
        {
            foreach (var destination in module.Value.Destinations)
            {
                if (!Modules.ContainsKey(destination))
                {
                    nonExistingDestinations.Add(destination);
                }
                else if (Modules[destination] is Conjunction)
                {
                    (Modules[destination] as Conjunction)?.RememberedPulses.Add(module.Key, false);
                }
            }
        }

        foreach (var destination in nonExistingDestinations)
        {
            Modules.Add(destination, new Module("", destination));
        }
    }

    public void Reset()
    {
        Modules.Clear();
        _lowPulseCounter = 0;
        _highPulseCounter = 0;
        _buttonCounter = 0;
        Cycles.Clear();
        _pleaseStop = false;
    }

    public long HitButtonXTimes(int times)
    {
        for (var i = 0; i < times; i++)
        {
            _buttonCounter++;
            HitThatButton();
        }
        return _lowPulseCounter * _highPulseCounter;
    }
    
    public long HitButtonUntil()
    {
        while (!_pleaseStop)
        {
            _buttonCounter++;
            HitThatButton();
        }

        return Numerics.Lcm(Cycles.Values.ToArray());
    }

    private void HitThatButton()
    {
        (Modules["broadcaster"] as BroadCaster)?.Push();

        do
        {
            PrepareNextStep();
            foreach (var module in Modules)
            {
                if (module.Value.ReceivedPulses.Count > 0)
                {
                    module.Value.HandleReceivedPulse();
                }
            }
        } while (Modules.Any(m => m.Value.ReceivedPulsesNext.Count > 0));
    }

    private void PrepareNextStep()
    {
        foreach (var module in Modules)
        {
            module.Value.ReceivedPulses = new Stack<(string, Pulse)>(module.Value.ReceivedPulsesNext);
            module.Value.ReceivedPulsesNext.Clear();
        }
    }

    private class FlipFlop : Module
    {
        public FlipFlop(string s, string n) : base(s, n)
        {
            On = false;
        }

        public override void ReceivePulse(Pulse pulse, string source)
        {
            if (pulse.High) _highPulseCounter++;
            else _lowPulseCounter++;
            ReceivedPulsesNext.Push((source, pulse));
        }

        public override void HandleReceivedPulse()
        {
            while (ReceivedPulses.Any())
            {
                var pulse = ReceivedPulses.Pop();

                if (!pulse.Item2.High)
                {
                    if (!On)
                    {
                        On = true;
                        foreach (var destination in Destinations)
                        {
                            Modules[destination].ReceivePulse(new Pulse(true), Name);
                        }
                    }
                    else
                    {
                        On = false;
                        foreach (var destination in Destinations)
                        {
                            Modules[destination].ReceivePulse(new Pulse(false), Name);
                        }
                    }
                }
            }
        }
    }

    private class Conjunction : Module
    {
        public Conjunction(string s, string n) : base(s, n)
        {
        }

        public readonly Dictionary<string, bool> RememberedPulses = new(); // set all to false

        public override void ReceivePulse(Pulse pulse, string source)
        {
            if (pulse.High)
            {
                _highPulseCounter++;
                
                if (Name == "lx")
                {
                    if (!Cycles.TryAdd(source, _buttonCounter))
                        _pleaseStop = true;
                }
            }
            else _lowPulseCounter++;

            ReceivedPulsesNext.Push((source, pulse));
        }

        public override void HandleReceivedPulse()
        {
            while (ReceivedPulses.Any())
            {
                var pulse = ReceivedPulses.Pop();
                RememberedPulses[pulse.Item1] = pulse.Item2.High;

                var allHigh = RememberedPulses.All(p => p.Value);

                foreach (var destination in Destinations)
                {
                    Modules[destination].ReceivePulse(allHigh ? new Pulse(false) : new Pulse(true), Name);
                }
            }
        }
    }

    private class BroadCaster : Module
    {
        public BroadCaster(string s) : base(s, "broadcaster")
        {
        }
        
        public void Push()
        {
            _lowPulseCounter++;
            foreach (var destinationName in Destinations)
            {
                SendPulse(false, destinationName);
            }
        }

        private void SendPulse(bool size, string destinationName)
        {
            var pulse = new Pulse(size);
            Modules[destinationName].ReceivePulse(pulse, "broadcaster");
        }
    }

    private class Module
    {
        public Module(string destinations, string name)
        {
            Destinations = destinations.Split(", ").ToList();
            Name = name;
        }

        public virtual void HandleReceivedPulse()
        {
        }

        public virtual void ReceivePulse(Pulse pulse, string source)
        {
            if (pulse.High) _highPulseCounter++;
            else
            {
                _lowPulseCounter++;
            }
        }

        protected readonly string Name;
        protected bool On;
        public readonly List<string> Destinations;
        public Stack<(string, Pulse)> ReceivedPulses = new();
        public readonly Stack<(string, Pulse)> ReceivedPulsesNext = new();
    }

    private class Pulse
    {
        public readonly bool High;

        public Pulse(bool high)
        {
            High = high;
        }
    }


}