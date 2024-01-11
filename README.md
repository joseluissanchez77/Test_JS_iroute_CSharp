
# Api clientes
Creada en visual studio 2022 , version de .Net 6


 


## API Reference

#### Lista todos los clientes

```http
  GET /api/Client
```


#### Lista clientes por identificacion

```http
  GET /api/Client?identificacion=0XXXXXXXXXX
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `identificacion` | `string` | **Required**. Numero de identificacion del cliente |

#### Crear un cliente

```http
  POST /api/Client
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `primerNombre`      | `string` | **Required**. primer nombre del cliente |
| `segundoNombre`      | `string` | **Optional**. segundo nombre del cliente|
| `apellidos`      | `string` | **Required**. apellidos del cliente|
| `identificacion`      | `string` | **Required**. identificacion  del cliente|
| `correo`      | `string` | **Required**. correo del cliente|

#### Inactivar un cliente

```http
  DELETE /api/Client/{id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id del cliente |



