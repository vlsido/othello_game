using System;


namespace MenuSystem
{
    public class MenuItem
    {
        public MenuItem(string title, Func<string>? runMethod)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentException("title cannot be empty!");

            Title = title.Trim();
            RunMethod = runMethod;
        }


        public string Title { get; private set; }

        public Func<string>? RunMethod { get; private set; }

        public override string ToString()
        {
            return Title;
        }
    }
}