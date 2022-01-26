using System;
using Xamarin.Forms;

namespace TrincaChurras.Views.Templates
{
    public class AgendaDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BarbecueTemplate { get; set; }
        public DataTemplate AddBarbecueTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item.ToString().Contains("Adicionar Churras"))
                return AddBarbecueTemplate;

            return BarbecueTemplate;
        }
    }
}
