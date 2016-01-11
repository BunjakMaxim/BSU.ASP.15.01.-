using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PhotoAlbum.Domain.Core;
using PhotoAlbum.Domain.Interface;
using PhotoAlbum.Domain.EF;

namespace PhotoAlbum.Domain.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly EFContext db;

        public TagRepository(EFContext contex)
        {
            db = contex;
        }

        public Tag GetDefoultTag()
        {
            return db.Tags.Find(1);
        }

        public IEnumerable<Photo> GetPhotosByTeg(string tagName)
        {
            return db.Tags.First(tag => tag.Name == tagName).Photos;
        }

        public IEnumerable<string> GetAllTagName()
        {
            return db.Tags.Select(n => n.Name);
        }

        public Tag NameInTag(string tagName)
        {
            return db.Tags.FirstOrDefault(t => t.Name.Equals(tagName, StringComparison.OrdinalIgnoreCase));
        }

        public void Create(Tag tag)
        {
            db.Tags.Add(tag);
        }
    }
}
