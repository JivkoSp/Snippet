# Snippet

Snippet is command-helper rest api with the purpose of helping the developer with finding all commands from specific group or searching for command name from group.
Each command have discription of how it's used and it's purpose.
Snippet allow's the expected grud operations and more specific updates with patch (besides put).

Authentication
Snippet has authentication mechanism with Jwt token and Identity api. When new user is registered 
the snippet api returns response in the form of json object: { Token : "jwtToken", Success: true }.
When the user sends subsecuent request to the snippet api he must send authorization header that includes: Bearer "user-token".

Allowed endpoints:

![image](https://user-images.githubusercontent.com/97282923/191720982-cabb07fa-25ad-4e60-a4c7-d1a78073617b.png)


Registration:
![image](https://user-images.githubusercontent.com/97282923/191724524-43aaac01-b4c9-4304-82cf-4e1c558b4648.png)
--Response: 
![image](https://user-images.githubusercontent.com/97282923/191724771-4f7c206c-f40d-453e-b8f7-fc63a8837453.png)

Login:
![image](https://user-images.githubusercontent.com/97282923/191724950-448ff968-2723-44f0-89be-8537a408dc95.png)
--Response:
![image](https://user-images.githubusercontent.com/97282923/191725058-040eb3e6-7140-4de8-a222-870074a48a01.png)

Request to endpoint with jwt token:
![image](https://user-images.githubusercontent.com/97282923/191725206-293f96ca-bf73-41f2-870b-69620f55f9af.png)

Request to endpoint without jwt token:
![image](https://user-images.githubusercontent.com/97282923/191725347-41620b62-8634-4231-a87b-c836ee34ce38.png)

Get all commands from group => GET snippet/groups/groupId/commands:
![image](https://user-images.githubusercontent.com/97282923/191727089-2adbdc21-3d83-4f22-aee8-b64b7c073fdc.png)

Patch command => PATCH snippet/groups/groupId/commands/commandId:
--The patch endpoint expects request in the form of:
{
  "op":"the operation" from Json Specification [Rfc 6902]=> {Add, Remove, Replace, Copy, Move, Test},
  "path":"/property to update",
  "value":"the new value"
}



