namespace _3DS_link_trade_bot
{
    public class Program
    {
        

        public static Form1 form1 = new Form1();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
         
            ApplicationConfiguration.Initialize();
            Application.Run(form1);
        }
    }
}