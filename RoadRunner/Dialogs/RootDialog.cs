using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using RoadRunner.Helpers;
using System.Linq;
using RoadRunner.Models;

namespace RoadRunner.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        [NonSerialized]
        private ResponceBuilder _responceBuilder;
        
        //private HandleRequestResult _storedRequestResult { get; set; }

        private ResponceBuilder ResponceBuilder
        {
            get
            {
                return _responceBuilder ?? (_responceBuilder = new ResponceBuilder());
            }
        }

        public RootDialog()
        {
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
            var location = activity.Entities?.Where(t => t.Type == "Place").Select(t => t.GetAs<Place>()).FirstOrDefault();

            var builderResult = ResponceBuilder.HandleRequest(activity.Text);

            // return our reply to the user
            await context.PostAsync($"{builderResult.TextToDisplay}");

            context.Wait(MessageReceivedAsync);
        }
    }
}