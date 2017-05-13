using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using RoadRunner.Helpers;

namespace RoadRunner.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        [NonSerialized]
        private ResponceBuilder _responceBuilder;

        public RootDialog()
        {
            _responceBuilder = new ResponceBuilder();
        }

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return

            var address = _responceBuilder.HandleRequest(activity.Text);

            // return our reply to the user
            await context.PostAsync($"Requested address : {address}");

            context.Wait(MessageReceivedAsync);
        }
    }
}