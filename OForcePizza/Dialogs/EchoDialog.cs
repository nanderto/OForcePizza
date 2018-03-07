using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace OForcePizza.Dialogs
{
    [Serializable]
    public class EchoDialog : IDialog<object>
    {
        protected int count = 1;

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as IMessageActivity;

            if (activity.Text == "reset")
            {
                PromptDialog.Confirm(context, 
                    AfterResetAsync, 
                    "Are you sure you want to reset the counter", 
                    "Sorry I dident get that",
                    promptStyle: PromptStyle.None);
            }
            else
            {
                await context.PostAsync($"{this.count++}: You said {activity.Text}");
                context.Wait(MessageReceivedAsync);
            }
        }

        private async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> result)
        {
            var confirm = await result;
            if (confirm)
            {
                this.count = 1;
                await context.PostAsync("Reset count.");
            }
            else
            {
                await context.PostAsync("Did not reset count.");
            }

            context.Wait(MessageReceivedAsync);

        }
    }
}