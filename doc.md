
**Channel message:**

API Endpoint: https://discord.com/api/v9/channels/878825516801589288/messages
Method: POST
Payload:
```json
{
  "content": "gn boyZZZZZ",
  "nonce": "949103759160508416",
  "tts": false 
}
```

Response:
```json
{
    "id": "949105538602528798",
    "type": 0,
    "content": "ZACK",
    "channel_id": "878825516801589288",
    "author": {
        "id": "120218905305284612",
        "username": "danvxz",
        "avatar": "74683424a69438f1c03b6737b703c4b8",
        "discriminator": "2630",
        "public_flags": 0
    },
    "attachments": [],
    "embeds": [],
    "mentions": [],
    "mention_roles": [],
    "pinned": false,
    "mention_everyone": false,
    "tts": false,
    "timestamp": "2022-03-04T00:46:33.931000+00:00",
    "edited_timestamp": null,
    "flags": 0,
    "components": [],
    "nonce": "949105540678549504",
    "referenced_message": null
}
```

Create Text Channel 
API: Endpoint https://discord.com/api/v9/guilds/486893722538213376/channels
Payload
```json
name: "ssss"
parent_id: "486893722538213378"
permission_overwrites: []
type: 0

```

Response: 
```json

{
    "id": "949396263307190392",
    "last_message_id": null,
    "type": 0,
    "name": "ssss",
    "position": 9,
    "parent_id": "486893722538213378",
    "topic": null,
    "guild_id": "486893722538213376",
    "permission_overwrites": [],
    "nsfw": false,
    "rate_limit_per_user": 0
}
```


Channel click on launch:
API Endpoint: https://discord.com/api/v9/channels/454388384555597824/messages?limit=50
```json
{
    "id": "949057746341331024",
    "type": 0,
    "content": "humma humma humma",
    "channel_id": "878825516801589288",
    "author": {
    "id": "837884174362869790",
    "username": "Math Class",
    "avatar": "02a1f7455ade3ac5b5659b7568a877c6",
    "discriminator": "5636",
    "public_flags": 0
},
    "attachments": [],
    "embeds": [],
    "mentions": [],
    "mention_roles": [],
    "pinned": false,
    "mention_everyone": false,
    "tts": false,
    "timestamp": "2022-03-03T21:36:39.368000+00:00",
    "edited_timestamp": null,
    "flags": 0,
    "components": []
}
```

	
**Private message:**
API Endpoint: 
https://discord.com/api/v9/channels/454388384555597824/messages
	
Payload:
```json
{
    content: "ignore test",
    nonce: "949103979244027904",
    tts: false
}
```
    
Response: 
```json
{
    "id": "949104747963617300",
    "type": 0,
    "content": "sorry ignore",
    "channel_id": "454388384555597824",
        "author": {
        "id": "120218905305284612",
        "username": "danvxz",
        "avatar": "74683424a69438f1c03b6737b703c4b8",
        "discriminator": "2630",
        "public_flags": 0
    },
    "attachments": [],
    "embeds": [],
    "mentions": [],
    "mention_roles": [],
    "pinned": false,
    "mention_everyone": false,
    "tts": false,
    "timestamp": "2022-03-04T00:43:25.428000+00:00",
    "edited_timestamp": null,
    "flags": 0,
    "components": [],
    "nonce": "949104750220017664",
    "referenced_message": null
}
```
    
On Private message click launch:
Response from API Endpoint: https://discord.com/api/v9/channels/856521737667870751/messages?limit=50
Response Payload:
	
```json
{
        "id": "948611406767468584",
        "type": 0,
        "content": "har inte g\u00e5tt igenom n\u00e5t f\u00f6r komplicerat",
        "channel_id": "454388384555597824",
        "author": {
            "id": "302754462320295946",
            "username": "victim",
            "avatar": "656d2530a8fd9a5f0b2d759d82ea2f83",
            "discriminator": "8152",
            "public_flags": 128
        },
        "attachments": [],
        "embeds": [],
        "mentions": [],
        "mention_roles": [],
        "pinned": false,
        "mention_everyone": false,
        "tts": false,
        "timestamp": "2022-03-02T16:03:03.721000+00:00",
        "edited_timestamp": null,
        "flags": 0,
        "components": []
}
```
    
Private Message Edit: 
Endpoint: https://discord.com/api/v9/channels/454388384555597824/messages/949104747963617300
Status: 200
Request method: PATCH

Payload: content: "sorry ignore aa"
    
Response: 
```json
{
        "id": "949104747963617300",
        "type": 0,
        "content": "sorry ignore aa",
        "channel_id": "454388384555597824",
        "author": {
            "id": "120218905305284612",
            "username": "danvxz",
            "avatar": "74683424a69438f1c03b6737b703c4b8",
            "discriminator": "2630",
            "public_flags": 0
        },
        "attachments": [],
        "embeds": [],
        "mentions": [],
        "mention_roles": [],
        "pinned": false,
        "mention_everyone": false,
        "tts": false,
        "timestamp": "2022-03-04T00:43:25.428000+00:00",
        "edited_timestamp": "2022-03-04T13:12:11.263495+00:00",
        "flags": 0,
        "components": []
    }
```
    
Private Message Delete: 
Endpoint: https://discord.com/api/v9/channels/454388384555597824/messages/949104747963617300
Status: 204
Request method: DELETE
Payload: No Content, no Response

Delete DM 
API: https://discord.com/api/v9/channels/854084229079105596
Response: 

```json
{
    "id": "854084229079105596",
    "type": 1,
    "last_message_id": "901873931777097838",
    "recipients": [
        {
            "id": "155149108183695360",
            "username": "Dyno",
            "avatar": "19a5ee4114b47195fcecc6646f2380b1",
            "discriminator": "3861",
            "public_flags": 589824,
            "bot": true
        }
    ]
}

```

Create DM
API Endpoint: https://discord.com/api/v9/users/@me/channels
```json 

{
    "id": "838086007437918259",
    "type": 1,
    "last_message_id": "943966816244469871",
    "recipients": [
        {
            "id": "837884174362869790",
            "username": "Math Class",
            "avatar": "02a1f7455ade3ac5b5659b7568a877c6",
            "discriminator": "5636",
            "public_flags": 0
        }
    ]
}

```