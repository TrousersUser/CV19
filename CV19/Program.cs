
using System;

namespace CV19
{
    public static class Program
    {
        [STAThread()]
        public static void Main(string[] args)
        {
            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
