namespace RentAds.Parser;

public interface IChannelsProvider
{
  IReadOnlyCollection<ChannelInfo> GetChannels();
}
