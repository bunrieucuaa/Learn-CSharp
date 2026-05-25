# Bài 49: MVC Mindset & Parameter Binding (Tư duy MVC và Liên kết tham số)

Khi viết Web API với ASP.NET Core, bạn sẽ được làm quen với một mô hình kiến trúc kinh điển làm nền tảng cho hầu hết các framework web hiện nay: **MVC (Model - View - Controller)**.

Bài học này sẽ giúp bạn định hình tư duy tổ chức code của một ứng dụng web API, hiểu rõ vai trò của **Controller** và cách cấu hình **Parameter Binding (Liên kết tham số)** để bóc tách dữ liệu từ HTTP Request map trực tiếp vào các đối tượng C#.

---

## 1. Mô hình MVC trong Web API

Mô hình MVC phân chia trách nhiệm hệ thống thành 3 thành phần chính:

* **Model (Mô hình):** Chứa cấu trúc dữ liệu và logic nghiệp vụ (Service, Entity).
* **View (Giao diện):** Trong ứng dụng Web API, Server **không trực tiếp vẽ giao diện** (không sinh HTML/CSS). Do đó, View thực chất được thay thế bằng **Client (ứng dụng Angular)** chạy trên trình duyệt của người dùng. Dữ liệu giao tiếp giữa Controller và View là các chuỗi **JSON**.
* **Controller (Bộ điều phối):** Là thành phần hứng trực tiếp các HTTP Request gửi đến từ Client. Controller phân tích Request, gọi Service (tầng Model) để xử lý nghiệp vụ, nhận kết quả và đóng gói thành HTTP Response (JSON) để trả về cho Client.

---

## 2. Parameter Binding (Ràng buộc tham số) là gì?

Khi Client gửi HTTP Request chứa dữ liệu (ví dụ: ID trên URL, từ khóa tìm kiếm trên Query String, JSON trong Body), làm thế nào để Controller C# nhận diện và tự động nạp các dữ liệu này vào các tham số của hàm?

ASP.NET Core cung cấp bộ các **Attributes (Chú thích đầu tham số)** để chỉ định rõ nguồn gốc lấy dữ liệu:

### 2.1. `[FromRoute]` (Lấy từ tham số đường dẫn)
Nạp dữ liệu từ Route parameter trên URL.
* *Ví dụ Route:* `/api/products/{id}`
* *Cú pháp:* `public IActionResult GetById([FromRoute] int id)`

### 2.2. `[FromQuery]` (Lấy từ chuỗi truy vấn)
Nạp dữ liệu từ Query string sau dấu `?`.
* *Ví dụ URL:* `/api/products?page=2&size=10`
* *Cú pháp:* `public IActionResult Get([FromQuery] int page, [FromQuery] int size)`

### 2.3. `[FromBody]` (Lấy từ thân Request)
Giải tuần tự hóa dữ liệu JSON từ Request Body thành đối tượng C#.
* *Cú pháp:* `public IActionResult Create([FromBody] ProductDto dto)`

### 2.4. `[FromHeader]` (Lấy từ Header)
Đọc giá trị từ một Header cụ thể (thường dùng để lấy Token bảo mật).
* *Cú pháp:* `public IActionResult SecureAction([FromHeader(Name = "Authorization")] string token)`

---

## 3. Kiểu trả về của Controller: `IActionResult`

Trong ASP.NET Core, các phương thức hành động (Actions) của Controller thường trả về interface **`IActionResult`** (hoặc lớp generic `ActionResult<T>`). Kiểu trả về này cho phép bạn dễ dàng đóng gói dữ liệu kèm theo mã trạng thái HTTP Status Code phù hợp:

* **`Ok(data)`**: Trả về dữ liệu kèm mã `200 OK`.
* **`Created(uri, data)`**: Trả về mã `201 Created` (thường dùng cho POST).
* **`BadRequest(errorMsg)`**: Trả về mã `400 Bad Request` khi dữ liệu đầu vào không hợp lệ.
* **`NotFound()`**: Trả về mã `404 Not Found` khi không tìm thấy tài nguyên.
* **`Unauthorized()`**: Trả về mã `401 Unauthorized` khi chưa đăng nhập.

### Ví dụ cấu trúc một Controller Web API chuẩn:
```csharp
[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet("{id}")] // GET /api/products/5
    public IActionResult GetById([FromRoute] int id)
    {
        var product = _service.GetById(id);
        if (product == null) return NotFound(); // Trả về 404
        
        return Ok(product); // Trả về 200 OK kèm dữ liệu
    }

    [HttpPost] // POST /api/products
    public IActionResult Create([FromBody] ProductCreateDto dto)
    {
        if (dto == null) return BadRequest("Dữ liệu không hợp lệ."); // Trả về 400
        
        var newProduct = _service.Create(dto);
        return Created($"api/products/{newProduct.Id}", newProduct); // Trả về 201
    }
}
```

---

## 4. Nguyên lý thiết kế: Skinny Controller (Controller mỏng)

> [!IMPORTANT]
> **Quy tắc bỏ túi:**
> * **Controller phải thật MỎNG (Skinny):** Nhiệm vụ duy nhất của Controller là hứng request, check validation cú pháp thô, điều phối gọi Service, và trả về Response.
> * **Service phải thật DÀY (Fat):** Toàn bộ logic nghiệp vụ, tính toán tiền, kiểm tra logic nghiệp vụ sâu phải nằm ở tầng Service.
> *Tuyệt đối không viết code tính toán logic nghiệp vụ hay code kết nối DB trực tiếp ở Controller.*

---

## 5. Checklist đánh giá hiểu bài

1. Hãy trình bày vai trò của Model, View, và Controller trong thiết kế ứng dụng Web API.
2. Phân biệt nguồn lấy dữ liệu của 3 chú thích: `[FromRoute]`, `[FromQuery]`, và `[FromBody]`.
3. Khi viết hàm cập nhật thông tin sản phẩm, tham số ID nên được lấy từ đâu (`[FromRoute]` hay `[FromBody]`)? Tại sao?
4. Trình bày ý nghĩa của nguyên lý thiết kế "Skinny Controller, Fat Service".
