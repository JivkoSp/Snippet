# Snippet

**Snippet** is a command-helper REST API designed to assist developers by providing the ability to find all commands from a specific group or search for a command name within a group. 
Each command includes a description of how it's used and its purpose. 
Snippet supports the standard CRUD operations and more specific updates with PATCH (besides PUT).

## Authentication

Snippet uses JWT token and Identity API for authentication. When a new user registers, the Snippet API returns a response in the form of a JSON object:

`json
{
  "Token": "jwtToken",
  "Success": true
}`

## Allowed endpoints:

![image](https://user-images.githubusercontent.com/97282923/191720982-cabb07fa-25ad-4e60-a4c7-d1a78073617b.png)


### Registration:
![image](https://user-images.githubusercontent.com/97282923/191724524-43aaac01-b4c9-4304-82cf-4e1c558b4648.png)
--Response: 
![image](https://user-images.githubusercontent.com/97282923/191724771-4f7c206c-f40d-453e-b8f7-fc63a8837453.png)

### Login:
![image](https://user-images.githubusercontent.com/97282923/191724950-448ff968-2723-44f0-89be-8537a408dc95.png)
--Response:
![image](https://user-images.githubusercontent.com/97282923/191725058-040eb3e6-7140-4de8-a222-870074a48a01.png)

### Request to endpoint with jwt token:
![image](https://user-images.githubusercontent.com/97282923/191725206-293f96ca-bf73-41f2-870b-69620f55f9af.png)

### Request to endpoint without jwt token:
![image](https://user-images.githubusercontent.com/97282923/191725347-41620b62-8634-4231-a87b-c836ee34ce38.png)

### Get all commands from group => GET snippet/groups/groupId/commands:
![image](https://user-images.githubusercontent.com/97282923/191727089-2adbdc21-3d83-4f22-aee8-b64b7c073fdc.png)

### Patch command 
#### Endpoint: PATCH /snippet/groups/{groupId}/commands/{commandId} 
#### The PATCH endpoint expects a request in the form of:
`{
  "op": "the operation",
  "path": "/property to update",
  "value": "the new value"
}`
#### Supported operations from RFC 6902 include: Add, Remove, Replace, Copy, Move, Test.
```{
"op":"the operation" from Json Specification [Rfc 6902]=> {Add, Remove, Replace, Copy, Move, Test},
"path":"/property to update",
"value":"the new value"
}

