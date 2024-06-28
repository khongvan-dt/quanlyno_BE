using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace quanLyNo.Controllers;
public interface IRepository<T>
{
    IEnumerable<T> Index();
    T GetById(int id);
    public IActionResult Delete(int id);
    // public IActionResult Create(T[] entity);
    public IActionResult Create(T entity);

    public IActionResult Update(T entity);
}
