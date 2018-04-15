namespace Leng.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Leng.Models.Concrete.BlogDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Leng.Models.Concrete.BlogDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            List<string> lists = new List<string>
            {
                "介绍",
                "单例模式是软件工程中最著名的模式之一。从本质上说，单例模式是一个只允许创建唯一实例的类，并提供获取该示例的简单方法。大多数情况下，单例模式不允许在创建实例时指定任何参数——否则对实例的第二个请求，但使用不同的参数可能会有问题!（如果需要对具有相同参数的所有请求都应访问同一实例，则工厂模式更合适。)本文只讨论不需要参数的情况。通常单例的一个要求是，它们是惰性创建的——也就是说，在第一次需要它时，该实例才会创建。",
                "在 C# 中实现单例模式有多种不同的方法。如下有几种实现方式，从最常见的、非线程安全的方式，到一个完全惰性加载、线程安全、简单和高性能的版本。",
                "然而，所有这些实现有如下四个共同特征:",
                "单个构造函数，它是私有和无参数的。这样可以防止其他类实例化它 (这将违反模式)。请注意，它还可以防止子类实例化它——如果单例可被子类实例化一次，则可以被子类实例化两次，并且它的所有子类都可以创建实例，这会违反该模式。如果需要基类型的单个实例，并且直到运行时才知道确切的类型的情况，则可以使用工厂模式。",
                "类是密封的。这并不是必需的，严格说来，密封类可能有助于 JIT 进行优化。",
                "一个静态变量，它保存对单个创建的实例的引用。",
                "一种公共静态方法，用于获取对单个创建实例的引用，如果示例为null，则创建一个。",
                "注意，所有这些实现还使用公共静态属性实例作为访问实例的手段。在任何情况下，属性都可以很容易地转换为方法，而不会对线程安全或性能产生任何影响。",
                "第一种——非线程安全方式",
                @"// Bad code! Do not use!
public sealed class Singleton
{
    private static Singleton instance=null;

    private Singleton()
    {
    }

    public static Singleton Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new Singleton();
            }
            return instance;
        }
    }
}",
                "上面的方式不是线程安全的。两个不同的线程可以同时判断 (实例 == null)，然后同时创建实例，这违反了单一模式。请注意，实际上在计判断表达式之前可能已经创建了实例，但是内存模型不能保证其他线程会知道实例的新值，除非已通过适当的方式在线程间共享了信息。",
                "第二种——简单线程安全方式",
               @"public sealed class Singleton
{
    private static Singleton instance = null;
    private static readonly object padlock = new object();

    Singleton()
    {
    }

    public static Singleton Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }
}",
                "此实现是线程安全的。该线程在共享对象上获取一个锁，然后在创建实例之前检查实例是否为null。这将处理内存共享问题 (因为锁确保了所有读取在获取后锁发生，并且解锁动作确保所有写入都在锁释放之前发生)，这将确保只有一个线程会创建实例 (仅一个线程可以位于代码中类的实例部分——到第二个线程进入它时，第一个线程已经创建了实例，因此表达式将计算为 false)。不幸的是每次请求实例时，性能都会受到锁的影响。",
                "请注意，不要将单利本身作为锁来实现，而是锁定该类的私有静态变量的值。锁定其他类可以访问的对象或是类型本身会极大影响性能甚至引发死锁。这是我一般情况下的首选方式——尽可能只锁定专门为锁定目的而创建的对象，或为特定目的而锁定的对象 (例如，等待/操作切换的队列)。通常这样的对象应该是私有的。这有助于简化编写线程安全的应用程序的流程。",
                "第三种——双判定带锁的方式",
@"// Bad code! Do not use!
public sealed class Singleton
{
    private static Singleton instance = null;
    private static readonly object padlock = new object();

    Singleton()
    {
    }

    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                }
            }
            return instance;
        }
    }
}",
                "此实现是线程安全的，而且不必每次都需要取出锁。不幸的是这种模式有四缺点:",
                "它在 Java 中行不通。这似乎是一个奇怪的事情，如果需要在 Java 中使用单例模式，则必须知道这一点。在将对新对象的引用分配给实例之前，Java 内存模型不确保构造函数已经完成。Java 1.5 对内存模型的此问题进行了重写，但在缺少volatile变量的情况下，双判定锁仍会失效。",
                "没有内存的隔离，它也违反了 ECMA CLI 规范。在. NET 2.0 内存模型 (它比 ECMA 规范更强) 的情况下，它很有可能是安全的，但我宁愿不依赖这些，特别是如果对安全有顾虑的话。",
                "很容易写错。该模式在if语句中有两个完全相同的判定，稍微错误的话可能会影响性能或是得不到正确实现。",
                "它没有下面几种方式简单优秀。",
                "第四种——非完全惰性不使用锁线程安全的方式",
@"public sealed class Singleton
{
    private static readonly Singleton instance = new Singleton();

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static Singleton()
    {
    }

    private Singleton()
    {
    }

    public static Singleton Instance
    {
        get
        {
            return instance;
        }
    }
}",

                "正如您所看到的，这确实非常简单——但为什么它是线程安全的，它为什么不是完全惰性呢？ C# 中的静态构造函数将确保在创建类的实例或引用静态成员时执行，并且每个 AppDomain 只执行一次。考虑到新构建的类型的检查需要执行任何其他操作，它将比在前面的示例中添加额外的检查速度更快。但是仍然有几个瑕疵:",
                "它不像其他实现那样完全惰性。特别是如果类有静态成员，则对这些成员的第一个引用将涉及创建实例。在下一个实现中更正了这一点。",
                "如果一个静态构造函数调用另一个，则会出现复杂的情况。. NET 规范 (第二章 9.5.3 节)有关于类型初始化的详细信息——它们大部分时候对你的程序不会有影响，但需要注意静态构造函数的循环调用。",
                "当类型没有用称为 beforefieldinit 的特殊标志标记时，类型初始值设定项的惰性只能由 . NET 保证。不幸的是，C# 编译器 (如. NET 1.1 运行时中提供的) 标记所有没有静态构造函数的类型 (即看起来像构造函数但标记为静态的块) 作为 beforefieldinit。我现在有一篇文章，详细介绍了这个问题。还要注意，它会影响性能，如本文结尾所讨论的那样。",
                "一种快捷方实现式 (只有这一个) 是将实例设置为公共静态只读成员，然后删除静态属性。这使得基本代码更加简化!然而，许多人更喜欢通过属性实现，预防将来需要修改，而且JIT 的内联优化很可能使性能相同。（请注意，如果需要惰性，静态构造函数本身仍然是必需的。)",
                "第五种——完全惰性实现",
@"public sealed class Singleton
{
    private Singleton()
    {
    }

    public static Singleton Instance { get { return Nested.instance; } }
        
    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly Singleton instance = new Singleton();
    }
}",
                "在这里，实例化是由对嵌套类的静态成员的第一个引用触发的，这仅在实例中才会发生这种情况，这意味着实现是完全惰性的，同时具有以前的所有性能好处。请注意，虽然嵌套类具有对封闭类的私有成员的访问权限，但反向没有，因此需要在此处实现实例化。同时这并没有引起任何其他问题，因为该类本身是私有的。但是，为了使实例化变得惰性，代码有点复杂。",
                "Sixth version - using .NET 4's Lazy<T> type",
                "如果使用. NET 4 (或更高)，则可以使用系统. 懒惰的<T>类型使懒惰真正简单。您需要做的就是将委托传递给调用单一构造函数的构造函数，这在 lambda 表达式中最容易完成.</T>",
@"public sealed class Singleton
{
    private static readonly Lazy<Singleton> lazy =
        new Lazy<Singleton>(() => new Singleton());
    
    public static Singleton Instance { get { return lazy.Value; } }

    private Singleton()
    {
    }
}",

                "它很简单，性能良好。如果需要，还允许您检查实例是否已创建 IsValueCreated 属性。",
                "Performance vs laziness",
                "在许多情况下，您实际上并不需要完全的懒惰-除非您的类初始化做了一些特别耗时的事情，或者在其他地方有某种副作用，否则最好不要使用上面显示的显式静态构造函数。这可能会提高性能，因为它允许 JIT 编译器进行单个检查 (例如，在方法的开头)，以确保该类型已初始化，然后从那时起假定它。如果在相对较紧的循环中引用了单个实例，则可能会产生 (相对) 显著的性能差异。您应该决定是否需要完全惰性实例化，并在类中适当地记录此决策。",
                "这一页存在的很多原因是人们试图聪明，从而提出了双重检查锁定算法。有一种态度，锁定是昂贵的，是常见的和误导。我写了一个非常快速的基准，它只是在循环10亿方法中获取单一实例，尝试不同的变体。这不是非常科学的，因为在现实生活中，你可能想知道如果每个迭代实际上涉及到一个调用的方法获取单例，那么它有多快。然而，它确实显示了一个重要的观点。在我的笔记本电脑上，最慢的解决方案 (约5的因素) 是锁定一个 (解决方案 2)。这很重要吗？可能没有，当你记住，它仍然设法获得10亿倍在40秒以下的单例。(注意: 这篇文章最初是在不久前写的--我预计现在的表现会更好。这意味着，如果您仅获取单件40万次每秒，获取成本将是1% 的性能-所以改进它不会做很多。现在，如果您正在获取的是通常的单例，那么您是否可能在循环中使用它呢？如果你关心的是提高性能一点点，为什么不声明一个局部变量在循环之外，获取单一，然后循环。宾果，即使最慢的实现也变得很容易。",
                "我会很感兴趣地看到一个真实的世界应用，其中使用简单的锁定和使用一个更快的解决方案实际上取得了显著的性能差异。",
                "Exceptions",
                "有时，您需要在单个构造函数中进行工作，这可能会引发异常，但对整个应用程序来说可能不是致命的。可能，您的应用程序可能能够解决此问题，并希望再次尝试。在这个阶段，使用类型初始值设定项来构造单例变得有问题。不同的运行时处理这种情况的方式不同，但我不知道有什么做所需的事情 (再次运行类型初始值设定项)，即使有人这样做，您的代码将打破在其他运行时。为了避免这些问题，我建议使用页面上列出的第二种模式-只需使用一个简单的锁，每次检查一次，在方法/属性中生成实例 (如果它尚未成功生成)。",
                "多亏了Andriy Tereshchenko提出这个问题。",
                "结论",
                "在 C# 中实现单例模式有多种不同的方法。一位读者给我写了一封详细说明他封装了同步方面的方法，虽然我承认在一些非常特殊的情况下可能有用 (特别是你想要非常高的性能，并且能够确定是否单例已创建，完全懒惰，而不考虑其他静态成员被调用)。我个人不认为这种情况会经常出现在这个页面上，但如果你在这种情况下，请给我发邮件。",
                "我的个人喜好是解决方案 4: 我通常会离开它的唯一时间是，如果我需要能够调用其他静态方法而不触发初始化，或如果我需要知道是否已实例化单例。我不记得我最后一次在这种情况下，假设我甚至有。在这种情况下，我可能会为解决方案 2，这仍然是好的，容易得到正确的。",
                "解决方案5是优雅的，但比2或4更棘手，正如我上面所说，它提供的好处似乎只是很少有用。如果使用. NET 4，解决方案6是实现懒惰的简单方法。它也有好处，它显然是懒惰的。我目前倾向于仍然使用解决方案 4，简单地通过习惯-但如果我与经验不足的开发者工作，我很可能会去为解决方案6开始作为一个简单和普遍适用的模式。",
                "(我不会使用解决方案 1，因为它是坏的，我不会使用解决方案 3，因为它没有好处超过5。"
            };


            context.Blogs.AddOrUpdate(m => m.Title,
                new Models.Entities.Blog
                {
                    Title = "C#中的单例模式",
                    Author = "Mawrence",
                    DateCreated = DateTime.Now,
                    Description = "单例模式是软件工程中最著名的模式之一。从本质上说，单例模式是一个只允许创建唯一实例的类，并提供获取该示例的简单方法。大多数情况下，单例模式不允许在创建实例时指定任何参数——否则对实例的第二个请求，但使用不同的参数可能会有问题!（如果需要对具有相同参数的所有请求都应访问同一实例，则工厂模式更合适。)本文只讨论不需要参数的情况。通常单例的一个要求是，它们是惰性创建的——也就是说，在第一次需要它时，该实例才会创建。",
                    Paragraphs = lists.Select(a => new Models.Entities.Paragraph
                    {
                        Text = a,
                        Author = "Mawrence",
                        Title = "C#中的单例模式",
                        Category = "p"

                    }).ToList()
                },
                new Models.Entities.Blog
                {
                    Title = "LINQ学习笔记",
                    Author = "Mawrence",
                    Description = "LINQ的一些学习笔记",
                    DateCreated = DateTime.Now
                });
        }
    }
}
