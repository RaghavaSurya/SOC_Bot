using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using SEVONEBot.Models;

namespace SEVONEBot
{
	[BotAuthentication]
	public class MessagesController : ApiController
	{
		/// <summary>
		/// POST: api/Messages
		/// Receive a message from a user and reply to it
		/// </summary>
		public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
		{
			if (activity.Type == ActivityTypes.Message)
			{
				await Conversation.SendAsync(activity, () => new EchoDialog());
			}
			else
			{
				HandleSystemMessage(activity);
			}
			var response = Request.CreateResponse(HttpStatusCode.OK);
			return response;
		}

		[Serializable]
		public class EchoDialog : IDialog<Object>
		{
			public async Task StartAsync(IDialogContext context)
			{
				context.Wait(MessageReceivedAsync);
			}

			public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
			{
				var message = await argument;
				if (message.Text.Contains("Hi") || message.Text.Contains("Hello") || message.Text == "Hi" || message.Text == "Hello" || message.Text == "Good afternoon" || message.Text == "Good Evening" || message.Text == "Good morning" || message.Text == "Good day")
				{
					await context.PostAsync(message.Text);
				}
				else if (message.Text.Contains("Incident"))
				{
					DataEmulator de = new DataEmulator();
					string s=de.GetData();
					message.Text = s;
					await context.PostAsync(message.Text);
				}
				else
				{
					message.Text = "Sorry Couldn't get you";
					await context.PostAsync(message.Text);
				}
				context.Wait(MessageReceivedAsync);
			}
		}

		private Activity HandleSystemMessage(Activity message)
		{
			if (message.Type == ActivityTypes.DeleteUserData)
			{
				// Implement user deletion here
				// If we handle user deletion, return a real message
			}
			else if (message.Type == ActivityTypes.ConversationUpdate)
			{
				Activity ac = new Activity();
				ac.Text = "Hi there";
				return ac;
			}
			else if (message.Type == ActivityTypes.ContactRelationUpdate)
			{
				// Handle add/remove from contact lists
				// Activity.From + Activity.Action represent what happened
			}
			else if (message.Type == ActivityTypes.Typing)
			{
				// Handle knowing tha the user is typing
			}
			else if (message.Type == ActivityTypes.Ping)
			{
			}

			return null;
		}
	}
}