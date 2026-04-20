using Microsoft.AspNetCore.Mvc;

namespace CafeBackend.Controllers {
    [ApiController] [Route("orders")]
    public class OrderController : ControllerBase {
        private static List<Order> orders = new();
        // Track Table Status (False = Vacant, True = Occupied)
        private static Dictionary<int, bool> tables = Enumerable.Range(1, 10).ToDictionary(i => i, i => false);

        [HttpGet] public IActionResult Get() => Ok(orders);

        [HttpGet("tables")] public IActionResult GetTables() => Ok(tables);

        [HttpPost] 
        public IActionResult Post([FromBody] Order o) { 
            o.Id = orders.Count + 1;
            orders.Add(o); 
            // Mark table as occupied if it's a Dine In order
            if (int.TryParse(o.TableNumber.Replace("Table ", ""), out int tNum)) {
                if (tables.ContainsKey(tNum)) tables[tNum] = true;
            }
            return Ok(o); 
        }

        [HttpDelete("{id}")] 
        public IActionResult Delete(int id) { 
            var order = orders.FirstOrDefault(x => x.Id == id);
            if (order != null) {
                // Free the table when order is deleted
                if (int.TryParse(order.TableNumber.Replace("Table ", ""), out int tNum)) {
                    if (tables.ContainsKey(tNum)) tables[tNum] = false;
                }
                orders.Remove(order);
            }
            return Ok(); 
        }
    }

    public class Order { 
        public int Id { get; set; } 
        public string CustomerName { get; set; } = "";
        public string TableNumber { get; set; } = "";
        public List<Item> Items { get; set; } = new();
        public double Total { get; set; }
    }
    public class Item { public string Name { get; set; } = ""; public int Qty { get; set; } public double Price { get; set; } }
}