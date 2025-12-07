using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace FinalProject.WorkOrders;

public class WorkOrderRepository
{
    private readonly Dictionary<int, WorkOrder> _ordersById = new();
    private readonly HashSet<string> _cities = new(StringComparer.OrdinalIgnoreCase);
    private readonly HashSet<string> _clients = new(StringComparer.OrdinalIgnoreCase);
    private readonly string _dataFilePath;

    public WorkOrderRepository(string dataFilePath)
    {
        _dataFilePath = dataFilePath;

        // Pre-populate common cities around Bellevue, WA (King County)
        _cities.Add("Bellevue");
        _cities.Add("Seattle");
        _cities.Add("Mercer Island");
        _cities.Add("Kirkland");
        _cities.Add("Redmond");
        _cities.Add("Sammamish");
        _cities.Add("Issaquah");
        _cities.Add("Renton");
        _cities.Add("Newcastle");
        _cities.Add("Bothell");
        _cities.Add("Kenmore");
        _cities.Add("Woodinville");
        _cities.Add("Shoreline");
        _cities.Add("Burien");
        _cities.Add("Tukwila");
        _cities.Add("SeaTac");
        _cities.Add("Kent");
        _cities.Add("Auburn");
    }

    public IReadOnlyDictionary<int, WorkOrder> OrdersById => _ordersById;
    public HashSet<string> Cities => _cities;
    public HashSet<string> Clients => _clients;

    public void Add(WorkOrder order)
    {
        _ordersById.Add(order.OrderNumber, order);
        _cities.Add(order.City);
        _clients.Add(order.ClientName);
    }

    public bool TryGet(int orderNumber, out WorkOrder? order)
    {
        return _ordersById.TryGetValue(orderNumber, out order);
    }

    public bool Remove(int orderNumber)
    {
        if (_ordersById.Remove(orderNumber, out var removed))
        {
            RebuildSets();
            return true;
        }
        return false;
    }

    public List<WorkOrder> GetAllAsList()
    {
        return _ordersById.Values.ToList();
    }

    public void Save()
    {
        var list = _ordersById.Values.ToList();
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(list, options);
        File.WriteAllText(_dataFilePath, json);
    }

    public void Load()
    {
        _ordersById.Clear();
        _cities.Clear();
        _clients.Clear();

        if (!File.Exists(_dataFilePath))
        {
            return;
        }

        var json = File.ReadAllText(_dataFilePath);
        var list = JsonSerializer.Deserialize<List<WorkOrder>>(json);
        if (list == null)
        {
            return;
        }

        foreach (var order in list)
        {
            _ordersById[order.OrderNumber] = order;
            _cities.Add(order.City);
            _clients.Add(order.ClientName);
        }
    }

    private void RebuildSets()
    {
        _cities.Clear();
        _clients.Clear();
        foreach (var order in _ordersById.Values)
        {
            _cities.Add(order.City);
            _clients.Add(order.ClientName);
        }
    }
}
