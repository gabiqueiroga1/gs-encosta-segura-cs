namespace EncostaSegura
{
    internal static class Program
    {

        [STAThread]
        private static void Main()
        {

            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}