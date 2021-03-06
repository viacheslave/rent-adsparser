# Lviv Rent Ads Telegram Bot

## Purpose

The bot attempts to collect, aggregate, de-duplicate and parse rental ads posted in popular Lviv Telegram channels. The data is then re-posted to subscribers.

Output posts contain the following key attributes (if successfully obtained):
- price
- rooms count
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
5. Links to OP don't work if end users aren't able to see OPs. 

## Requirements and usage

- Telegram Account, Application and Bot
- .NET 6
- Docker

Docker container requires an external volume where the app stores its DBs, logs and a Telegram session file. The example below uses `external` folder on the filesystem:
1. Build Docker a image: `docker build . -t rentads`.
2. Run: `docker run docker run -it --mount type=bind,source="$(pwd)"/external,target=/usr/src/app/external rentads:latest`

Currently, the app is run at https://t.me/lvivrent2bot

## Settings

1. The app asks for a working Telegram application credentials on startup. Make sure you have the following attributes available: **api_key**, **api_hash**, **phone_number**, **password** (2FA).

2. Set the actual bot `token` in the `appsettings.json` file. The token is obtained when the actual bot is created.