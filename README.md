# Lviv Rent Ads Bot

## Purpose

The bot attempts to collect, aggregate, de-duplicate and parse rental ads posted in popular Lviv Telegram channels. The data is then re-posted to subscribers.

Output posts contain the following key attributes (if successfully obtained):
- price
- rooms number
- size
- location
- appartment complex name
- date
- link to OP

## Sources

Currently, the sources are:
- https://t.me/orendakvartyr_ua
- https://t.me/orenda_nomakler_lviv
- https://t.me/nerukhomist_orenda_prodazh
- https://t.me/rieltor_lviv1
- https://t.me/neruhomist_lviv_ua
- https://t.me/neryhomist_lviv1
- https://t.me/nerukhomistlviv
- https://t.me/RealtyLviv
- https://t.me/orenda_lviv
- https://t.me/Arenda_posutochno_Lviv

## Limitations

1. Due to the variety in OP style and syntax, oftentimes the attributes are missing.
2. The OPs with multiple proposals may not be parsed correctly.
3. Special characters are not parsed out.
4. Pictures are not re-posted.
5. Links to OP don't work when end users are not able to see OPs. 