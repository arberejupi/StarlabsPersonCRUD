using FluentValidation;
using StarlabsCRUD.Data;
using StarlabsCRUD.Models;

namespace StarlabsCRUD.Services
{
    public class ValidationException: Exception
    {
        public List <string> Errors { get; set; }
        public ValidationException(List<string> errors): base ("Validation failed")
        {
            Errors = errors;
        }
    }
    public class PersonService
    {
        private readonly DataContext _dataContext;
        private readonly IValidator<Person> _personValidator;
        
        public PersonService(DataContext dataContext, IValidator<Person> personValidator)
        {
            _dataContext = dataContext;
            _personValidator= personValidator;
        }

        public void AddPerson(Person person)
        {
            var validationResult = _personValidator.Validate(person);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.Select(error => error.ErrorMessage).ToList());
            }

            try
            {
                _dataContext.Person.Add(person);
                _dataContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error adding person to datbase");
            }
        }

        // Add this method to your PersonService class
        public void UpdatePerson(int personId, Person updatedPerson)
        {
            var existingPerson = _dataContext.Person.Find(personId);

            if (existingPerson == null)
            {
                throw new ApplicationException($"Person with ID {personId} not found.");
            }

            var validationResult = _personValidator.Validate(updatedPerson);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.Select(error => error.ErrorMessage).ToList());
            }

            try
            {
                existingPerson.Name = updatedPerson.Name;
                existingPerson.Surname = updatedPerson.Surname;
                existingPerson.Age = updatedPerson.Age;

                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error updating person in the database", ex);
            }
        }

        public void DeletePerson(int personId)
        {
            var existingPerson = _dataContext.Person.Find(personId);

            if (existingPerson == null)
            {
                throw new ApplicationException($"Person with ID {personId} not found.");
            }

            try
            {
                _dataContext.Person.Remove(existingPerson);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error deleting person from the database", ex);
            }
        }

    }
}
