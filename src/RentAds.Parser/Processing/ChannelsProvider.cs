namespace RentAds.Parser;

internal class ChannelsProvider : IChannelsProvider
{
  public IReadOnlyCollection<ChannelInfo> GetChannels()
  {
    // seed
    var channels = new List<ChannelInfo>
    {
      new (1491309367, 2217106488608026049),  // "orenda_nomakler_lviv", 
      new (1479272596, -4093352904629687407), // "orendakvartyr_ua", 
      new (1386111122, 6266838702911673179),  // "nerukhomist_orenda_prodazh", 
      new (1542899168, 9155470683363337665),  // "rieltor_lviv1", 
      new (1215779157, 7685024642326068376),  // "nerukhomistlviv", 
      new (1636479189, 1371029993909710689),  // "orenda_lviv", 
      new (1490010580, 5477157463430450583),  // "RealtyLviv", 
      new (1661831881, -7851889272683553453), // "neryhomist_lviv1", 
      new (1356075188, -7388729293266140382), // "neruhomist_lviv_ua", 
      new (1317233739, 7892408786034274642),  // "Arenda_posutochno_Lviv", 
    };

    return channels;
  }
}
