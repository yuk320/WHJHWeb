###auto publish tools README.MD

` npm run auto [startDate] `

- preauto is npm i ; it will npm install all node_modules

- startDate **default** = lastVersion's date  if lastVersion's date is null **default** will be today

`config.json `

- **lastVersion** need rewrite last version by hand
- **version** need rewrite current version by hand
- **mode** has **UpdateOnly**、**CompleteOnly**、**All** to selection; **UpdateOnly** will only create update package; **CompleteOnly** will only create full package; **All** will create both.
- **otherRoomCard** be true will copy roomcard site to **WHJHRoomCard** filder form **WHJHFront**
- **otherAPI** be true will copy api site to WHJHAPI **filder** form **WHJHFront**
- **versionList** will be auto rewrite by index script.
