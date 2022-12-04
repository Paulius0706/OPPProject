namespace BlazorGame.Game.Command
{
    public class CommandInvoker
    {
        private Command command;
        public void SetCommand(Command command)
        {
            this.command = command;
        }
        public void ExecuteCommand()
        {
            command.Execute();
        }
    }
}
