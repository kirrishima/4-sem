using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.ComponentModel;
using EF_Core.Models;
using EF_Core.Data;
using System.Threading;

namespace EF_Core
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppDbContext _context;

        public MainWindow()
        {
            InitializeComponent();

            InitContext();
            InitDataGrids();
        }

        private void InitContext()
        {

            // Инициализация контекста и БД
            _context = new AppDbContext();
            _context.Database.EnsureCreated();     // создаст БД при первом запуске
            _context.Categories.Load();           // загружаем категории в локальный контекст
            _context.Products.Load();             // загружаем товары в локальный контекст
        }

        private void InitDataGrids()
        {
            // Привязка таблиц DataGrid к локальным коллекциям EF Core
            categoryDataGrid.ItemsSource = _context.Categories.Local.ToObservableCollection();
            productDataGrid.ItemsSource = _context.Products.Local.ToObservableCollection();
        }

        // Освобождение контекста при закрытии окна
        protected override void OnClosing(CancelEventArgs e)
        {
            _context.Dispose();
            base.OnClosing(e);
        }

        // Добавление новой категории (её нужно будет именовать в таблице)
        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            _context.Categories.Add(new Category { Name = "Новая категория" });
        }

        // Удаление выбранной категории
        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (categoryDataGrid.SelectedItem is Category category)
            {
                _context.Categories.Remove(category);
            }
        }

        // Сохранение изменений (добавление/редактирование/удаление категорий)
        private async void SaveCategories_Click(object sender, RoutedEventArgs e)
        {
            await _context.SaveChangesAsync();  // асинхронно сохраняем в БД&#8203;:contentReference[oaicite:16]{index=16}
            categoryDataGrid.Items.Refresh();
        }


        //3. Пояснения по откату изменений
        //3.1 Детач новых сущностей
        //При entry.State == EntityState.Added перевод в Detached удаляет запись из отслеживания контекста без сохранения в базу, тем самым откатывая «добавление» до сохранения
        //Stack Overflow
        //.

        //3.2 Reload для модификаций и удалений
        //Метод entry.Reload() (а в асинхронном варианте ReloadAsync()) повторно запрашивает данные из базы и заменяет текущие значения свойств, переводя сущность в состояние Unchanged
        //Microsoft Learn
        //.Это восстанавливает любые изменения или отменяет удаление.

        //3.3 Обновление UI
        //После изменения состояния всех сущностей достаточно вызвать DataGrid.Items.Refresh(), чтобы перерисовать таблицы и показать актуальные данные из Local.ToObservableCollection().

        /// <summary>
        /// Откат несохранённых изменений категорий:
        /// Added → Detached; Modified/Deleted → Reload
        /// </summary>
        private void ResetCategories_Click(object sender, RoutedEventArgs e)
        {
            foreach (var entry in _context.ChangeTracker
                                          .Entries<Category>()
                                          .ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;    // убираем новые несохранённые записи :contentReference[oaicite:0]{index=0}
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();                        // перезагружаем данные из БД :contentReference[oaicite:1]{index=1}
                        break;
                }
            }
            categoryDataGrid.Items.Refresh();
        }

        // Добавление нового товара для выбранной категории
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (categoryDataGrid.SelectedItem is Category category)
            {
                _context.Products.Add(new Product
                {
                    Name = "Новый товар",
                    CategoryId = category.CategoryId
                });
            }
        }

        // Удаление выбранного товара
        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (productDataGrid.SelectedItem is Product product)
            {
                _context.Products.Remove(product);
            }
        }

        // Сохранение изменений по товарам
        private async void SaveProducts_Click(object sender, RoutedEventArgs e)
        {
            await _context.SaveChangesAsync();  // асинхронное сохранение&#8203;:contentReference[oaicite:17]{index=17}
            productDataGrid.Items.Refresh();
        }

        /// <summary>
        /// Откат несохранённых изменений товаров:
        /// Added → Detached; Modified/Deleted → Reload
        /// </summary>
        private void ResetProducts_Click(object sender, RoutedEventArgs e)
        {
            foreach (var entry in _context.ChangeTracker
                                          .Entries<Product>()
                                          .ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;    // удаляем несохранённые новые элементы :contentReference[oaicite:2]{index=2}
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();                        // подгружаем актуальные данные из БД :contentReference[oaicite:3]{index=3}
                        break;
                }
            }
            productDataGrid.Items.Refresh();
        }
    }
}