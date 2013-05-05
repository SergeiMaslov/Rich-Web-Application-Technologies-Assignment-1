var chat;

function initChat()
{
	chat = $.connection.ChatHub;

	chat.client.addNewMessage = function (time, username, newMessage)
	{
		renderMessageToPage(time, username, newMessage, $("#chatDiscussion"));
	};

	chat.client.doCommand = function (cmd)
	{
		if (cmd == "login")
		{
			$("#chatDiscussion").html("");
			showLoginDialog();
		}
	}

	$.connection.hub.start().done(function ()
	{
		setTimeout(function () { chat.server.getHistory(); }, 500);
	});
}

function sendMessageToServer()
{
	var newMessage = $("#newMessageTextArea").val();
	chat.server.send(newMessage);

	$("#newMessageTextArea").val("");
}


function renderMessageToPage(time, username, newMessage, divToPutData)
{
	$(divToPutData).append("<span class='ts'>" + time + "</span>&nbsp;&nbsp;&nbsp;<img src='http://www.gravatar.com/avatar/" + $.md5(username) + "?s=16&d=identicon&r=PG'>&nbsp;"
		+ "<strong>" + username + "</strong>:&nbsp;&nbsp;" + newMessage + "<br/>");
	$("#chatDiv").scrollTop($(divToPutData).height());
}
/*
function getChatHistory()
{
	var username = $("#username").val();

	$.post('/Chat/GetChatHistory', { "username": username },
		function (data)
		{

			for (var i = 0; i < data.length; i++)
			{
				console.log(data[i].Username);
				renderMessageToPage(data[i].Username,
									data[i].Message,
									$("#chatHistory"));
			}
		});
}
*/


function showLoginDialog()
{
	$("#chatInputContainer, #logout").css("visibility", "hidden");

	$("#divLoginLink").hide();
	$("#divSignupLink").show();
	showDialog("Login");
}

function showRegisterDialog()
{
	$("#divSignupLink").hide();
	$("#divLoginLink").show();
	showDialog("Sign up");
}

function showDialog(title)
{
	var dlg = $("#dialog");

	if (dlg.dialog("isOpen"))
		dlg.dialog("close");

	dlg.dialog("option", "title", title);
	$("#txLogin, #txPassword").val("");
	$("#dialogMessage").text("");

	dlg.dialog("open");
	$(".ui-dialog-titlebar-close").remove();

	$("#txLogin").focus();
}

function logout()
{
	$.post("Chat/Logout",
		null,
		function (data)
		{
			document.location.reload();
		}
	);
}


$(document).ready(function ()
{
	//$("#chat").click(function () { getChatHistory(); });

	$("#chatForm").submit(function ()
	{
		try
		{
			var msg = $.trim($("#newMessageTextArea").val());
			if (msg.length > 0)
				sendMessageToServer();
			else
				$("#newMessageTextArea").val("");

			$("#newMessageTextArea").focus();
		}
		catch (e)
		{
			alert(e);
		}

		return false;
	});

	$("#dialog").dialog(
	{
		modal: true,
		autoOpen: false,
		show: "blind",
		draggable: false,
		resizable: false,
		closeOnEscape: false,
		title: "Login",
		width: 300,
		minHeight: 20
	});

	$("#dialogForm").submit(function ()
	{
		$("#dialogMessage").text();

		if ($("#txLogin").val().length == 0)
		{
			$("#txLogin").focus();
			return;
		}
		
		if ($("#txPassword").val().length == 0)
		{
			$("#txPassword").focus();
			return;
		}

		$.post("Chat/" + ($("#dialog").dialog("option", "title") == "Login" ? "Login" : "Signup"),
			{ "username": $("#txLogin").val(), "password": $("#txPassword").val() },
			function (data)
			{
				if (data == "ok")
				{
					$("#dialog").dialog("close");
					//$("#username").val(data.substr(3));
					$("#chatInputContainer, #logout").css("visibility", "visible");
					initChat();
					$("#newMessageTextArea").focus();
				}
				else
				{
					$("#dialogMessage").text(data);
				}
			}
		);
	});

	if (isLoggedIn)
		initChat();
	//else
	//	$("#chatInputContainer, #logout").css("visibility", "hidden");

});
