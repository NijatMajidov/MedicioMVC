using MedicioMVC.Business.Exceptions;
using MedicioMVC.Business.Services.Abstract;
using MedicioMVC.Core.Models;
using MedicioMVC.Core.RepositoryAbstract;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicioMVC.Business.Services.Concretes
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DoctorService(IDoctorRepository doctorRepository,IWebHostEnvironment webHostEnvironment )
        {
            _doctorRepository = doctorRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public void CreateDoctor(Doctor doctor)
        {
            if (doctor == null) throw new EntityNullException("", "Doctor object null exception!");
            if (doctor.ImgFile == null) throw new FileNullException("ImgFile","Image File Null exception!");
            if (!doctor.ImgFile.ContentType.Contains("image/")) throw new FileContentTypeException("ImgFile","File Content Type exception!");
            if (doctor.ImgFile.Length > 2097152) throw new FileSizeException("ImgFile", "File Size exception. Size<2Mb!!");
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(doctor.ImgFile.FileName);
            string path = _webHostEnvironment.WebRootPath + @"\uploads\doctors\" + filename;
            using ( FileStream stream = new FileStream(path, FileMode.Create))
            {
                doctor.ImgFile.CopyTo(stream);
            }
            doctor.ImgUrl = filename;
            _doctorRepository.Add(doctor);
            _doctorRepository.Commit();
        }

        public void DeleteDoctor(int id)
        {
            var exsistDoctor = _doctorRepository.Get(x=>x.Id == id);
            if (exsistDoctor == null) throw new EntityNullException("", "This id not correct!!!");
            string path = _webHostEnvironment.WebRootPath + @"\uploads\doctors\" + exsistDoctor.ImgUrl;
            if (!File.Exists(path)) throw new ImageFileNotFoundException("ImgFile", "Image File Not Found!");
            File.Delete(path);
            _doctorRepository.Delete(exsistDoctor);
            _doctorRepository.Commit();
        }

        public List<Doctor> GetAllDoctors(Func<Doctor, bool>? func = null)
        {
            return _doctorRepository.GetAll(func);
        }

        public Doctor GetDoctor(Func<Doctor, bool>? func = null)
        {
            return _doctorRepository.Get(func);
        }

        public void UpdateDoctor(int id, Doctor doctor)
        {
            var oldDoctor = _doctorRepository.Get(x=>x.Id == id);
            if (oldDoctor == null) throw new EntityNullException("", "This Id Not Correct!!!");


            if (doctor.ImgFile != null)
            {
                if (!doctor.ImgFile.ContentType.Contains("image/")) throw new FileContentTypeException("ImgFile", "File Content Type exception!");
                if (doctor.ImgFile.Length > 2097152) throw new FileSizeException("ImgFile", "File Size exception. Size<2Mb!!");
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(doctor.ImgFile.FileName);
                string path = _webHostEnvironment.WebRootPath + @"\uploads\doctors\" + filename;
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    doctor.ImgFile.CopyTo(stream);
                }
                oldDoctor.ImgUrl = filename;
            }

            oldDoctor.Name = doctor.Name;
            oldDoctor.Surname = doctor.Surname;
            oldDoctor.Position = doctor.Position;
            _doctorRepository.Commit();
        }
    }
}
