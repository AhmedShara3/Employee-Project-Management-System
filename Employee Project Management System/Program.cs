using Employee_Project_Management_System.Data;
using Employee_Project_Management_System.Models;
using Microsoft.EntityFrameworkCore;


namespace Employee_Project_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var context = new AppDbContext();

            
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            
            bool looping = false;
            

            while(!looping)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Employee Project Management System");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("1. Manage Employees");
                Console.WriteLine("2. Manage Departments");
                Console.WriteLine("3. Manage Projects");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ManageEmployees(context);
                        break;
                    case "2":
                        ManageDepartments(context);
                        break;
                    case "3":
                        ManageProjects(context);
                        break;
                    case "4":
                        looping = true;
                        break;

                }

            }

            static void ManageDepartments(AppDbContext context)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("=== Manage Departments ===");
                    Console.WriteLine("1. Add Department");
                    Console.WriteLine("2. Display Departments");
                    Console.WriteLine("3. Edit Department");
                    Console.WriteLine("4. Delete Department");
                    Console.WriteLine("5. Back to Main Menu");
                    Console.WriteLine("Choose an option: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("Adding new department...");
                            Console.WriteLine("Enter department name:");
                            string name = Console.ReadLine();

                            context.Departments.Add(new Department { Name = name });
                            context.SaveChanges();

                            Console.WriteLine("Department added successfully!");
                            break;
                        case "2":
                            Console.Clear();
                            var displayDepartments = context.Departments.Include(d => d.Employees).ToList();
                            if (displayDepartments.Count == 0)
                            {
                                Console.WriteLine("No departments found.");
                            }
                            Console.WriteLine("\n === Departments ===");

                            
                            foreach (var department in displayDepartments)
                            {
                                 Console.WriteLine($" Department Name: {department.Name}");
                                Console.WriteLine("Employees :");
                                if (department.Employees != null && department.Employees.Any())
                                {
                                    var empName = department.Employees.Select(e => e.Name);
                                    Console.WriteLine(string.Join(", ", empName));


                                }
                                else
                                {
                                    Console.WriteLine("No employees in this department.");
                                }
                                Console.WriteLine("\nPress any key to continue...");
                                Console.ReadKey();
                            }

                            break;
                        case "3":
                            Console.Clear();
                            Console.WriteLine("Editing department...");
                            var deptsToEdit = context.Departments.ToList();
                            if (deptsToEdit.Count == 0)
                            {
                                Console.WriteLine("No departments found.");
                                break;
                            }
                            var editDept = new Dictionary<int, int>();
                            int index = 1;

                            Console.WriteLine("Select a department to edit:");
                            foreach(var d in deptsToEdit)
                            {
                                Console.WriteLine($"{index}. {d.Name}");
                                editDept[index] = d.Id;
                                index++;
                            }
                            Console.WriteLine("Enter the number of the department to edit:");
                            int selectedIndex = int.Parse(Console.ReadLine());

                            int realIndex = editDept[selectedIndex];
                            var departmentToEdit = context.Departments.Find(realIndex);

                            Console.WriteLine("Current department name: {0}", departmentToEdit.Name);
                            Console.WriteLine("Enter new department name:");
                            string newName = Console.ReadLine();
                            departmentToEdit.Name = newName;
                            context.SaveChanges();
                            Console.WriteLine("Department updated successfully!");
                            break;
                        case "4":
                            Console.Clear();
                            Console.WriteLine("Deleting department...");
                            var deptsToDelete = context.Departments.ToList();
                            if (deptsToDelete.Count == 0)
                            {
                                Console.WriteLine("No departments found.");
                                break;
                            }
                            var deleteDept = new Dictionary<int, int>();
                            index = 1;
                            Console.WriteLine("Select a department to delete:");
                            foreach (var d in deptsToDelete)
                            {
                                Console.WriteLine($"{index}. {d.Name}");
                                deleteDept[index] = d.Id;
                                index++;
                            }
                            Console.WriteLine("Enter the number of the department to delete:");
                            selectedIndex = int.Parse(Console.ReadLine());
                            realIndex = deleteDept[selectedIndex];
                            var departmentToDelete = context.Departments.Find(realIndex);
                            context.Departments.Remove(departmentToDelete);
                            context.SaveChanges();
                            Console.WriteLine("Department deleted successfully!");
                            break;
                        case "5":
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;

                    }


                }
            }
            static void ManageProjects(AppDbContext context)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("=== Manage Projects ===");
                    Console.WriteLine("1. Add Project");
                    Console.WriteLine("2. Display Projects");
                    Console.WriteLine("3. Edit Project");
                    Console.WriteLine("4. Delete Project");
                    Console.WriteLine("5. Back to Main Menu");
                    Console.WriteLine("Choose an option: ");
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("Adding new project...");
                            Console.WriteLine("Enter project name:");
                            string name = Console.ReadLine();
                            context.Projects.Add(new Project { Name = name });
                            context.SaveChanges();
                            Console.WriteLine("Project added successfully!!!");

                            break;
                        case "2":
                            Console.Clear();
                            Console.WriteLine("Displaying projects...");
                            var displayProjects = context.Projects.Include(p => p.Employees).ToList();
                            foreach(var p in displayProjects)
                            {
                                Console.WriteLine($"Project: {p.Name}");
                                Console.WriteLine("Employees:");

                                if (p.Employees != null && p.Employees.Any())
                                {
                                    var empName = p.Employees.Select(e => e.Name);
                                    Console.WriteLine(string.Join(", ", empName));

                                }
                                else
                                {
                                    Console.WriteLine("No employees assigned to this project.");
                                }
                            }
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();
                            break;
                        case "3":
                            Console.Clear();
                            Console.WriteLine("Editing project...");
                            var editProjects = context.Projects.ToList();
                            if (editProjects.Count == 0)
                            {
                                Console.WriteLine("No projects found.");
                                break;
                            }
                            var editProject = new Dictionary<int, int>();
                            int index = 1;

                            Console.WriteLine("Select a project to edit:");
                            foreach (var p in editProjects)
                            {
                                Console.WriteLine($"{index}. {p.Name}");
                                editProject[index] = p.Id;
                                index++;
                            }
                            Console.WriteLine("Enter the number of the project you want to edit:");
                            int selectedIndex = int.Parse(Console.ReadLine());
                            int realIndex = editProject[selectedIndex];

                            Console.WriteLine("Current project name: {0}", context.Projects.Find(realIndex).Name);
                            Console.WriteLine("Enter new project name:");
                            string newName = Console.ReadLine();
                            var projectToEdit = context.Projects.Find(realIndex);
                            projectToEdit.Name = newName;
                            context.SaveChanges();
                            Console.WriteLine("Project edited successfully!!!");

                            
                            break;
                        case "4":
                            Console.WriteLine("Deleting project...");
                            var deleteProjects = context.Projects.ToList();
                            if (deleteProjects.Count == 0)
                            {
                                Console.WriteLine("No projects found.");
                                break;
                            }
                            var deleteProject = new Dictionary<int, int>();
                            index = 1;

                            Console.WriteLine("Select a project to delete:");
                            foreach (var p in deleteProjects)
                            {
                                Console.WriteLine($"{index}. {p.Name}");
                                deleteProject[index] = p.Id;
                                index++;
                            }
                            Console.WriteLine("Enter the number of the project you want to delete:");
                            selectedIndex = int.Parse(Console.ReadLine());
                            realIndex = deleteProject[selectedIndex];

                            var projectToDelete = context.Projects.Find(realIndex);
                            context.Projects.Remove(projectToDelete);
                            context.SaveChanges();
                            Console.WriteLine("Project deleted successfully!!!");
                            break;
                        case "5":
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
            }

            static void ManageEmployees(AppDbContext context)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("=== Manage Employees ===");
                    Console.WriteLine("1. Add Employee");
                    Console.WriteLine("2. Display Employees");
                    Console.WriteLine("3. Edit Employee");
                    Console.WriteLine("4. Delete Employee");
                    Console.WriteLine("5. Back to Main Menu");
                    Console.WriteLine("Choose an option: ");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter Employee Name: ");
                            string newName = Console.ReadLine();

                            var departments = context.Departments.ToList();
                            if (departments.Count == 0)
                            {
                                Console.WriteLine("Please add a department first from the Main Menu!");
                                break;
                            }

                            var deptDict = new Dictionary<int, int>();
                            int deptSerial = 1;
                            Console.WriteLine("\nSelect a Department for this employee:");
                            foreach (var d in departments)
                            {
                                Console.WriteLine($"{deptSerial}. {d.Name}");
                                deptDict.Add(deptSerial, d.Id);
                                deptSerial++;
                            }

                            Console.Write("Enter Department Number: ");
                            if (int.TryParse(Console.ReadLine(), out int selectedDeptNum) && deptDict.ContainsKey(selectedDeptNum))
                            {
                      
                                int actualDeptId = deptDict[selectedDeptNum];

                           
                                var selectedDept = context.Departments.Find(actualDeptId);

                                
                                var newEmp = new Employee
                                {
                                    Name = newName,
                                    Department = selectedDept 
                                };

                                
                                context.Employees.Add(newEmp);
                                context.SaveChanges();
                                Console.WriteLine("Employee added successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Invalid selection.");
                            }
                            break;

                        case "2":
                            Console.Clear();
                            var displayEmployees = context.Employees
                                              .Include(e => e.Department)
                                              .Include(e => e.Projects)
                                              .ToList();

                            if (displayEmployees.Count == 0)
                            {
                                Console.WriteLine("No employees found.");
                                break;
                            }

                            Console.WriteLine("\n--- Employees List ---");
                            foreach (var e in displayEmployees)
                            {
                                Console.WriteLine($"Name: {e.Name}");
                                Console.WriteLine($"  Department: {e.Department?.Name ?? "No Department"}");

                                Console.Write("  Projects: ");
                                if (e.Projects != null && e.Projects.Any())
                                {
                                    var projectNames = e.Projects.Select(p => p.Name);
                                    Console.WriteLine(string.Join(", ", projectNames));
                                }
                                else
                                {
                                    Console.WriteLine("None");
                                }
                                Console.WriteLine("-------------------------");
                                Console.WriteLine("\nPress any key to continue...");
                                Console.ReadKey();
                            }
                            break;
                            case "3":
                            var allEmployees = context.Employees
                                          .Include(e => e.Department)
                                          .Include(e => e.Projects)
                                          .ToList();

                            if (allEmployees.Count == 0)
                            {
                                Console.WriteLine("No employees found.");
                                break;
                            }

                            var empDict = new Dictionary<int, int>();
                            int empSerial = 1;
                            Console.WriteLine("\nSelect Employee to Edit:");
                            foreach (var e in allEmployees)
                            {
                                Console.WriteLine($"{empSerial}. {e.Name} (Dept: {e.Department?.Name})");
                                empDict.Add(empSerial, e.Id);
                                empSerial++;
                            }

                            Console.Write("Enter Employee Number: ");
                            if (int.TryParse(Console.ReadLine(), out int selectedEmpNum) && empDict.ContainsKey(selectedEmpNum))
                            {
                                int actualEmpId = empDict[selectedEmpNum];
                                var empToEdit = allEmployees.First(e => e.Id == actualEmpId);

                                Console.WriteLine("\n--- Edit Options ---");
                                Console.WriteLine("1. Edit Employee Name");
                                Console.WriteLine("2. Assign to a different Department");
                                Console.WriteLine("3. Assign to a Project");
                                Console.WriteLine("4. Remove from a Project");
                                Console.Write("Select edit option: ");
                                string editChoice = Console.ReadLine();

                                if (editChoice == "1") 
                                {
                                    Console.WriteLine($"\nCurrent Name: {empToEdit.Name}");
                                    Console.Write("Enter new Name (or press Enter to keep current): ");
                                    string updatedName = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(updatedName))
                                    {
                                        empToEdit.Name = updatedName;
                                    }
                                }
                                else if (editChoice == "2") 
                                {
                                    var depts = context.Departments.ToList();
                                    var dDict = new Dictionary<int, int>();
                                    int dSerial = 1;
                                    Console.WriteLine("\nSelect New Department:");
                                    foreach (var d in depts)
                                    {
                                        Console.WriteLine($"{dSerial}. {d.Name}");
                                        dDict.Add(dSerial, d.Id);
                                        dSerial++;
                                    }
                                    Console.Write("Enter Department Number: ");
                                    if (int.TryParse(Console.ReadLine(), out int dNum) && dDict.ContainsKey(dNum))
                                    {
                                        empToEdit.DepartmentId = dDict[dNum];
                                    }
                                }
                                else if (editChoice == "3") 
                                {
                                    var availableProjects = context.Projects
                                        .Where(p => !empToEdit.Projects.Contains(p)).ToList();

                                    if (availableProjects.Count == 0)
                                    {
                                        Console.WriteLine("No available projects to assign.");
                                        break;
                                    }

                                    var pDict = new Dictionary<int, int>();
                                    int pSerial = 1;
                                    Console.WriteLine("\nSelect Project to Assign:");
                                    foreach (var p in availableProjects)
                                    {
                                        Console.WriteLine($"{pSerial}. {p.Name}");
                                        pDict.Add(pSerial, p.Id);
                                        pSerial++;
                                    }
                                    Console.Write("Enter Project Number: ");
                                    if (int.TryParse(Console.ReadLine(), out int pNum) && pDict.ContainsKey(pNum))
                                    {
                                        var projToAdd = context.Projects.Find(pDict[pNum]);
                                        empToEdit.Projects.Add(projToAdd);
                                    }
                                }
                                else if (editChoice == "4") 
                                {
                                    if (empToEdit.Projects.Count == 0)
                                    {
                                        Console.WriteLine("Employee is not assigned to any projects.");
                                        break;
                                    }

                                    var pDict = new Dictionary<int, int>();
                                    int pSerial = 1;
                                    Console.WriteLine("\nSelect Project to Remove from:");
                                    foreach (var p in empToEdit.Projects)
                                    {
                                        Console.WriteLine($"{pSerial}. {p.Name}");
                                        pDict.Add(pSerial, p.Id);
                                        pSerial++;
                                    }
                                    Console.Write("Enter Project Number: ");
                                    if (int.TryParse(Console.ReadLine(), out int pNum) && pDict.ContainsKey(pNum))
                                    {
                                        var projToRemove = empToEdit.Projects.First(p => p.Id == pDict[pNum]);
                                        empToEdit.Projects.Remove(projToRemove);
                                    }
                                }

                                context.SaveChanges();
                                Console.WriteLine("Employee updated successfully!");
                            }
                            break;
                        case "4":
                            var empsToDelete = context.Employees.ToList();
                            if (empsToDelete.Count == 0)
                            {
                                Console.WriteLine("No employees found.");
                                break;
                            }

                            var delDict = new Dictionary<int, int>();
                            int delSerial = 1;
                            Console.WriteLine("\nSelect Employee to Delete:");
                            foreach (var e in empsToDelete)
                            {
                                Console.WriteLine($"{delSerial}. {e.Name}");
                                delDict.Add(delSerial, e.Id);
                                delSerial++;
                            }
                            Console.Write("Enter Employee Number: ");
                            if (int.TryParse(Console.ReadLine(), out int delNum) && delDict.ContainsKey(delNum))
                            {
                                var empToRemove = context.Employees.Find(delDict[delNum]);
                                context.Employees.Remove(empToRemove);
                                context.SaveChanges();
                                Console.WriteLine("Employee deleted successfully!");
                            }
                            break;
                            case "5":
                            return;
                        default:
                            Console.WriteLine("Invalid option, try again.");
                            break;


                    }

                }
            }
        }
    }
}
