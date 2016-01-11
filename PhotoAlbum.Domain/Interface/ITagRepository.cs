using System;
using System.Collections.Generic;
using PhotoAlbum.Domain.Core;

namespace PhotoAlbum.Domain.Interface
{
    public interface ITagRepository
    {
        IEnumerable<Photo> GetPhotosByTeg(string tagName);
        IEnumerable<string> GetAllTagName();
        Tag GetDefoultTag();
        Tag NameInTag(string tagName);
        void Create(Tag tag);
    }
}
