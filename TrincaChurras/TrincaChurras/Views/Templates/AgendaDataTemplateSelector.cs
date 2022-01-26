using System;
using TrincaChurras.Models;
using Xamarin.Forms;

namespace TrincaChurras.Views.Templates
{
    public class AgendaDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BarbecueTemplate { get; set; }
        public DataTemplate AddBarbecueTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var bbq = (Barbecue)item;
            return bbq.Title == "Adicionar Churras" ? AddBarbecueTemplate : BarbecueTemplate;
        }
    }
}
