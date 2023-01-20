using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetsController(ApplicationContext context) {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<Pet> GetPets() {
            return _context.Pets
                .Include(pet => pet.petOwner);
        }

        [HttpPut("{id}/checkout")]
        public Pet petCheckout(int id) 
        {
            Pet pet = _context.Pets.Find(id);
            pet.checkedInAt = null;
            _context.Pets.Update(pet);
            _context.SaveChanges();
            return pet;

        }  

        [HttpPut("{id}/checkin")]
        public Pet petCheckIn(int id) 
        {
            Pet pet = _context.Pets.Find(id);
            pet.checkedInAt = DateTime.Now;
            _context.Pets.Update(pet);
            _context.SaveChanges();
            return pet;

        }      


        [HttpPost]
        public Pet postPet(Pet pet)
        {
            _context.Pets.Add(pet);
            _context.SaveChanges();
            return pet;
        }

        [HttpDelete("{id}")]
        public Pet deletePet(int id)
        {
            Pet foundPet = _context.Pets.Find(id);
            _context.Pets.Remove(foundPet);
            _context.SaveChanges();
            return foundPet;
        }
    }
}
