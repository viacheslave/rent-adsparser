namespace RentAds.Parser.Telegram;

internal static class PostBuilder
{
  public static Post Build(TL.MessageBase message)
  {
    if (message is not TL.Message)
    {
      return new Post(message.ID, message.Peer.ID, string.Empty, message.Date);
    }

    var tlMessage = (TL.Message)message;
    if (tlMessage.message == string.Empty)
    {
      return new Post(message.ID, message.Peer.ID, string.Empty, message.Date);
    }

    var post = new Post(tlMessage.ID, tlMessage.Peer.ID, tlMessage.message, tlMessage.Date);

    var reference = GetReference(tlMessage);
    if (reference.HasValue)
    {
      post.SetOriginal(reference.Value.id, reference.Value.peer);
    }

    return post;
  }

  private static (long peer, long id)? GetReference(TL.Message tlMessage)
  {
    var fromPeer = tlMessage.From;

    if (fromPeer is null)
    {
      return null;
    }

    var peer = fromPeer switch
    {
      TL.PeerUser fromPeerUser => fromPeerUser.user_id,
      TL.PeerChannel fromPeerChannel => fromPeerChannel.channel_id,
      _ => (long?)null
    };

    if (peer == null)
    {
      return null;
    }

    var id = tlMessage.fwd_from switch
    {
      TL.MessageFwdHeader header => header.channel_post,
      _ => 0
    };

    return (peer.Value, id);
  }

  /*
    if (tlMessage.flags.HasFlag(Message.Flags.has_fwd_from) && tlMessage.fwd_from is not null)
    {
      Messages_MessagesBase forwardedMessages = null;

      var fromPeer = tlMessage.From;
      if (fromPeer is TL.PeerUser fromPeerUser)
      {
        InputUserFromMessage inputUserFromMessage = new InputUserFromMessage
        {
          peer = peer,
          msg_id = tlMessage.id,
          user_id = fromPeer.ID
        };

        return null;
      }
      else
      {
        InputChannelBase inputChannelFromMessage = new InputChannelFromMessage
        {
          peer = peer,
          msg_id = tlMessage.id,
          channel_id = tlMessage.from_id.ID
        };

        forwardedMessages = await client.Channels_GetMessages(
          inputChannelFromMessage,
          new[] { new InputMessageID { id = tlMessage.fwd_from.saved_from_msg_id } });

        if (forwardedMessages.Count == 0)
        {
          return null;
        }

        var fwd = forwardedMessages.Messages[0];

        if (fwd is TL.Message fwdMessage && fwdMessage.message != "")
        {
          return new Post(fwdMessage.Peer.ID, fwdMessage.ID, tlMessage.message);
        }

        return null;
      }
    }
    */
}
