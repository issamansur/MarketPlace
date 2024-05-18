TODO:
- Handle all errors in `WebAPI`
- Add `Swagger` docs
- See all `ToDo`s and refactor

Thoughts:
- Use `Domain events` instead of `Tenant`
- Use `identity`/`JWT`/Table with rights and remove `ChangerId` from all commands
- Use `ImageMagick`/`ImwgeSharp` to crop image, saving by `ImageService` and return it by new type of request
- Think about CORRECT update/delete and using `Patch` instead of/with `Put`
- Use `.UseQuartz()` to deactivate/expire `UserAdvertisement`s
- Use more flexible way to configure configuration and place for static files
(We also can to take a look about Cloud solutions)