### auto publish tools README.MD

` npm run auto [startDate] `

- preauto is npm i ; it will npm install all node_modules

- startDate **default** = lastVersion's date (config.json:new Date(versionList[lastVersion]))  if lastVersion's date is null **default** will be today

`config.json `

- **product_name** will be headfix in your packaged fileName.
- **lastVersion** need rewrite last version by hand
- **version** need rewrite current version by hand
- **mode** has **PublishOnly**、**UpdateOnly**、**CompleteOnly**、**All** to selection;**PublishOnly** will only compire the code, **UpdateOnly** will only create update package; **CompleteOnly** will only create full package; **All** will create both.
- **otherRoomCard** be true will copy roomcard site to **WHJHRoomCard** filder form **WHJHFront**
- **otherAPI** be true will copy api site to WHJHAPI **filder** form **WHJHFront**
- **versionList** will be auto rewrite by index script.

[MobileApiDoc](./Docs/Mobile.MD "MobileApiDoc")
[SpreadHome](./Docs/SpreadHome.MD "SpreadHome")
