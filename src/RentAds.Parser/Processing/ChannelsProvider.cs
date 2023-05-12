namespace RentAds.Parser;

internal class ChannelsProvider : IChannelsProvider
{
  public IReadOnlyCollection<ChannelInfo> GetChannels()
  {
    // seed
    var channels = new List<ChannelInfo>
    {
      new (1490010580, 5477157463430450583 ), //:: НЕРУХОМІСТЬ ОРЕНДА ПРОДАЖ ::  - Group @RealtyLviv
      new (1227632175, 5625223052408300180 ), //:: Оренда. Квартири. Кімнати. Львів. ::  - Group @orendakvartyrlviv
      new (1323432594, -2042037370101220169), //:: 🏠🏠🏠 НЕРУХОМІСТЬ №1 Львів Оренда :: - Group @lviv_neruhomist
      new (1636479189, 1371029993909710689 ), //:: Актуальна Оренда Львів - Real HiT :: - Group @orenda_lviv
      new (1379672244, -8431100289203013585), //:: Оренда Житлова Львів :: - Group @LvivsjkaOrenda
      new (1386111122, 6266838702911673179 ), //:: НЕРУХОМІСТЬ МІСТА ЛЕВА 🦁 :: - Group @nerukhomist_orenda_prodazh
      new (1356075188, -7388729293266140382), //:: Нерухомість Львів :: - Group @neruhomist_lviv_ua
      new (1542899168, 9155470683363337665 ), //:: 🇺🇦 Нерухомість Львів🔹Оренда🔹Продаж :: - Group @rieltor_lviv1
      new (1712904519, -1311285278714786261), //:: ЖИТЛО ЛЬВІВ ⬇️ :: - Group @lviv_orenda
      new (1401696309, 8325248055542275162 ), //:: ОРЕНДА КВАРТИР ЛЬВІВ :: - Group @orendakvarturlviv
      new (1215779157, 7685024642326068376 ), //:: Оренда, продаж | Львів :: - Group @neruhomistlviv_ua
      new (1661831881, -7851889272683553453), //:: ПОДОБОВА ДОВГОТРИВАЛА ЛЬВІВ Chat :: - Group @neryhomist_lviv1
      new (1759116607, 2455609233782295060 ), //:: ОРЕНДА КВАРТИР ЛЬВІВ :: - Channel @orendakvarturlviv1
      new (1491309367, 2217106488608026049 ), //:: Нерухомість від власника. Львів :: - Group @orenda_nomakler_lviv
      new (1479272596, -4093352904629687407), //:: Оренда від Віктора :: - Channel @orendakvartyr_ua
      new (1647812911, -2824907617669942657), //:: ОРЕНДА ЛЬВІВ Квартири та Будинки :: - Channel @lvivOG
      new (1317233739, 7892408786034274642 ), //:: Житло оренда Львів / Західна :: - Group @Arenda_posutochno_Lviv
      new (1121023329, -8687563731659139193), //:: ОРЕНДА КВАРТИР НЕРУХОМІСТЬ ЛЬВІВ :: - Channel @arenda_nerukhomist
      new (1714631754, 1345083091765869499 ), //:: Варіанти житла у Львові :: - Channel @HelpHomeLviv
    };

    return channels;
  }
}
