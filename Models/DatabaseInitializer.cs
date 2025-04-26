    using System;
    using System.Linq;
namespace AymanProject.Models
{

    public class DatabaseInitializer
    {
        public static void Initialize(EvaluationContext context)
        {
            context.Database.EnsureCreated();

            if (context.MainCriterians.Any())
            {
                return; // Database has been seeded
            }

            // Main Criteria
            var mainCriteria = new MainCriterian[]
            {
            new MainCriterian { Id = 1, Text_Ar = "القضاء على الفقر بجميع أشكاله في كل مكان", Text_En = "End poverty in all its forms everywhere", Weight = 0.269 },
            new MainCriterian { Id = 2, Text_Ar = "القضاء على الجوع وتوفير الأمن الغذائي والتغذية المحسنة وتعزيز الزراعة المستدامة", Text_En = "End hunger, achieve food security and improved nutrition and promote sustainable agriculture", Weight = 0.187 },
            new MainCriterian { Id = 3, Text_Ar = "ضمان حياة صحية وتعزيز الرفاهية للجميع في جميع الأعمار", Text_En = "Ensure healthy lives and promote well-being for all at all ages", Weight = 0.148 },
            new MainCriterian { Id = 4, Text_Ar = "ضمان التعليم الجيد المنصف والشامل وتعزيز فرص التعلم مدى الحياة للجميع", Text_En = "Ensure inclusive and equitable quality education and promote lifelong learning opportunities for all", Weight = 0.118 },
            new MainCriterian { Id = 5, Text_Ar = "تحقيق المساواة بين الجنسين وتمكين جميع النساء والفتيات", Text_En = "Achieve gender equality and empower all women and girls", Weight = 0.009 },
            new MainCriterian { Id = 6, Text_Ar = "ضمان توافر المياه وخدمات الصرف الصحي للجميع وإدارتها بشكل مستدام", Text_En = "Ensure availability and sustainable management of water and sanitation for all", Weight = 0.019 },
            new MainCriterian { Id = 7, Text_Ar = "ضمان الوصول إلى طاقة حديثة موثوقة ومستدامة وبأسعار معقولة للجميع", Text_En = "Ensure access to affordable, reliable, sustainable and modern energy for all", Weight = 0.007 },
            new MainCriterian { Id = 8, Text_Ar = "تعزيز النمو الاقتصادي المطرد والشامل للجميع والعمالة الكاملة والمنتجة وتوفير العمل اللائق للجميع", Text_En = "Promote sustained, inclusive and sustainable economic growth, full and productive employment and decent work for all", Weight = 0.027 },
            new MainCriterian { Id = 9, Text_Ar = "بناء بنية تحتية مرنة وتعزيز التصنيع الشامل للجميع والمستدام وتشجيع الابتكار", Text_En = "Build resilient infrastructure, promote inclusive and sustainable industrialization and foster innovation", Weight = 0.011 },
            new MainCriterian { Id = 10, Text_Ar = "الحد من عدم المساواة داخل البلدان وفيما بينها", Text_En = "Reduce inequality within and among countries", Weight = 0.004 },
            new MainCriterian { Id = 11, Text_Ar = "جعل المدن والمستوطنات البشرية شاملة للجميع وآمنة ومرنة ومستدامة", Text_En = "Make cities and human settlements inclusive, safe, resilient and sustainable", Weight = 0.057 },
            new MainCriterian { Id = 12, Text_Ar = "ضمان أنماط الاستهلاك والإنتاج المستدامة", Text_En = "Ensure sustainable consumption and production patterns", Weight = 0.006 },
            new MainCriterian { Id = 13, Text_Ar = "اتخاذ إجراءات عاجلة لمكافحة تغير المناخ وآثاره", Text_En = "Take urgent action to combat climate change and its impacts", Weight = 0.015 },
            new MainCriterian { Id = 14, Text_Ar = "حفظ المحيطات والبحار والموارد البحرية واستخدامها على نحو مستدام لتحقيق التنمية المستدامة", Text_En = "Conserve and sustainably use the oceans, seas and marine resources for sustainable development", Weight = 0.003 },
            new MainCriterian { Id = 15, Text_Ar = "حماية النظم الإيكولوجية البرية وترميمها وتعزيز استخدامها على نحو مستدام، وإدارة الغابات على نحو مستدام، ومكافحة التصحر، ووقف تدهور الأراضي وعكس مساره، ووقف فقدان التنوع البيولوجي", Text_En = "Protect, restore and promote sustainable use of terrestrial ecosystems, sustainably manage forests, combat desertification, and halt and reverse land degradation and halt biodiversity loss", Weight = 0.005 },
            new MainCriterian { Id = 16, Text_Ar = "تعزيز المجتمعات السلمية والشاملة للجميع من أجل تحقيق التنمية المستدامة، وتوفير إمكانية الوصول إلى العدالة للجميع، وبناء مؤسسات فعالة وخاضعة للمساءلة وشاملة للجميع على جميع المستويات", Text_En = "Promote peaceful and inclusive societies for sustainable development, provide access to justice for all and build effective, accountable and inclusive institutions at all levels", Weight = 0.038 },
            new MainCriterian { Id = 17, Text_Ar = "تعزيز وسائل التنفيذ وتنشيط الشراكة العالمية من أجل تحقيق التنمية المستدامة", Text_En = "Strengthen the means of implementation and revitalize the global partnership for sustainable development", Weight = 0.078 }
            };

            context.MainCriterians.AddRange(mainCriteria);
            context.SaveChanges();

            // Sub Criteria
            var subCriteria = new SubCriterian[]
            {
            // No Poverty (1)
            new SubCriterian { MainId = 1, Text_Ar = "إيجاد فرص عمل للفقراء", Text_En = "Finding job opportunities for the poor", Weight = 0.331 },
            new SubCriterian { MainId = 1, Text_Ar = "الوصول إلى الخدمات الأساسية (الماء، الكهرباء، الطرق)", Text_En = "Access to basic services (water, electricity, roads)", Weight = 0.207 },
            new SubCriterian { MainId = 1, Text_Ar = "المشاريع التي تساهم في دعم وتعزيز سبل العيش للفقراء", Text_En = "Projects contribute to supporting and enhancing the livelihoods of the poor", Weight = 0.107 },
            new SubCriterian { MainId = 1, Text_Ar = "نجاح المشروع في تحسين مستويات الدخل للفقراء", Text_En = "Project success in improving the income levels of the poor", Weight = 0.077 },
            new SubCriterian { MainId = 1, Text_Ar = "تقليل التفاوت في الدخل بين الفقراء والأغنياء", Text_En = "Reducing income disparity between the poor and the rich", Weight = 0.059 },
            new SubCriterian { MainId = 1, Text_Ar = "مشاركة الفقراء في عملية التخطيط", Text_En = "Participation of the poor in the planning process", Weight = 0.034 },
            new SubCriterian { MainId = 1, Text_Ar = "الحفاظ على الموارد الطبيعية للفقراء", Text_En = "Preserving natural resources for the poor", Weight = 0.040 },
            new SubCriterian { MainId = 1, Text_Ar = "الوصول إلى الرعاية الصحية والتعليم للفقراء", Text_En = "Access to health care and education for the poor", Weight = 0.146 },
            
            // Zero Hunger (2)
            new SubCriterian { MainId = 2, Text_Ar = "توفير غذاء صحي لعمال المشروع", Text_En = "Providing healthy food for project workers", Weight = 0.151 },
            new SubCriterian { MainId = 2, Text_Ar = "الوصول إلى مياه نظيفة وآمنة", Text_En = "Access to clean and safe water", Weight = 0.342 },
            new SubCriterian { MainId = 2, Text_Ar = "ممارسات وتقنيات الزراعة المستدامة", Text_En = "Sustainable Agricultural Practices Techniques", Weight = 0.079 },
            new SubCriterian { MainId = 2, Text_Ar = "تعزيز الإنتاجية الزراعية", Text_En = "Enhancing agricultural productivity", Weight = 0.042 },
            new SubCriterian { MainId = 2, Text_Ar = "دعم المزارعين والرعاة الصغار", Text_En = "Supporting small farmers and herders", Weight = 0.214 },
            new SubCriterian { MainId = 2, Text_Ar = "مساهمة المشاريع في تنويع المحاصيل الزراعية", Text_En = "Contribution of projects to diversification of agricultural crops", Weight = 0.111 },
            new SubCriterian { MainId = 2, Text_Ar = "الحفاظ على التنوع البيولوجي في نجاح المشروع", Text_En = "Maintaining biodiversity in project success", Weight = 0.061 },
            
            // Good Health and Well-Being (3)
            new SubCriterian { MainId = 3, Text_Ar = "توفير الخدمات الصحية للعمال", Text_En = "Providing health services for workers", Weight = 0.079 },
            new SubCriterian { MainId = 3, Text_Ar = "برامج التثقيف الصحي والوقاية للعاملين في مشاريع البنية التحتية", Text_En = "Health education and prevention programs for workers in infrastructure projects", Weight = 0.064 },
            new SubCriterian { MainId = 3, Text_Ar = "ضمان بيئة عمل آمنة وصحية", Text_En = "Ensuring a safe and healthy work environment", Weight = 0.225 },
            new SubCriterian { MainId = 3, Text_Ar = "الوقاية من الأمراض المرتبطة بالعمل", Text_En = "Prevention of work-related diseases", Weight = 0.171 },
            new SubCriterian { MainId = 3, Text_Ar = "توفير برامج صحية للعمال", Text_En = "Providing health programs for workers", Weight = 0.061 },
            new SubCriterian { MainId = 3, Text_Ar = "الوصول إلى المرافق الصحية الأساسية", Text_En = "Access to basic health facilities", Weight = 0.113 },
            new SubCriterian { MainId = 3, Text_Ar = "تقليل مخاطر الآثار السلبية للكوارث", Text_En = "Reducing the risks and negative impacts of disasters", Weight = 0.035 },
            
            // Quality Education (4)
            new SubCriterian { MainId = 4, Text_Ar = "توفير فرص تعليمية متساوية للعمال", Text_En = "Providing equal educational opportunities for workers", Weight = 0.153 },
            new SubCriterian { MainId = 4, Text_Ar = "توفير الوصول إلى التعليم المهني والتقني للعمال", Text_En = "Providing access to vocational and technical education for workers", Weight = 0.132 },
            new SubCriterian { MainId = 4, Text_Ar = "تحسين مهارات القوى العاملة", Text_En = "Improving workforce skills", Weight = 0.192 },
            new SubCriterian { MainId = 4, Text_Ar = "ربط التعليم باحتياجات سوق العمل", Text_En = "Linking education to the needs of the labor market", Weight = 0.286 },
            new SubCriterian { MainId = 4, Text_Ar = "توفير فرص التدريب المهني لعمال المشروع", Text_En = "Providing vocational training opportunities for project workers", Weight = 0.237 },
            
            // Gender Equality (5)
            new SubCriterian { MainId = 5, Text_Ar = "مدى تكافؤ الفرص بين النساء والرجال", Text_En = "The extent of equal opportunities between women and men", Weight = 0.127 },
            new SubCriterian { MainId = 5, Text_Ar = "المساواة في الوصول إلى التدريب والتطوير والمكافآت والحوافز", Text_En = "Equality in access to training and development and rewards and incentives", Weight = 0.109 },
            new SubCriterian { MainId = 5, Text_Ar = "مشاركة المرأة في صنع القرار", Text_En = "Women's participation in decision-making", Weight = 0.166 },
            new SubCriterian { MainId = 5, Text_Ar = "السلامة والحماية للمرأة في بيئة العمل", Text_En = "Safety and protection for women in the work environment", Weight = 0.241 },
            new SubCriterian { MainId = 5, Text_Ar = "تلبية احتياجات المرأة في تصميم المشروع", Text_En = "Meeting women's needs in project design", Weight = 0.357 },
            
            // Clean Water and Sanitation (6)
            new SubCriterian { MainId = 6, Text_Ar = "الوصول إلى مياه نظيفة وبأسعار معقولة للسكان", Text_En = "Access to clean and affordable water for the population", Weight = 0.336 },
            new SubCriterian { MainId = 6, Text_Ar = "الوصول إلى مرافق الصرف الصحي", Text_En = "Access to sanitation facilities", Weight = 0.174 },
            new SubCriterian { MainId = 6, Text_Ar = "الإدارة الآمنة والصديقة للبيئة لمياه الصرف الصحي والنفايات الصلبة", Text_En = "Safe and environmentally friendly wastewater and solid waste management", Weight = 0.063 },
            new SubCriterian { MainId = 6, Text_Ar = "كفاءة استخدام المياه وإدارة الموارد المائية في مشاريع البنية التحتية", Text_En = "Water use efficiency and water resource management in infrastructure projects", Weight = 0.091 },
            new SubCriterian { MainId = 6, Text_Ar = "مدى التكامل والتنسيق بين إدارة المياه وإدارة النفايات في مشاريع البنية التحتية", Text_En = "Extent of integration and coordination between water management and waste management in infrastructure projects", Weight = 0.260 },
            new SubCriterian { MainId = 6, Text_Ar = "استخدام التكنولوجيا الحديثة والمبتكرة في مشاريع البنية التحتية للمياه والصرف الصحي", Text_En = "Use of modern and innovative technology in water and sanitation infrastructure projects", Weight = 0.139 },
            
            // Affordable and Clean Energy (7)
            new SubCriterian { MainId = 7, Text_Ar = "الوصول إلى الكهرباء للسكان في مشاريع البنية التحتية", Text_En = "Access to electricity for the population in infrastructure projects", Weight = 0.322 },
            new SubCriterian { MainId = 7, Text_Ar = "معدل وكفاءة الطاقة المتجددة في مشاريع البنية التحتية", Text_En = "Rate and efficiency of renewable energy in infrastructure projects", Weight = 0.255 },
            new SubCriterian { MainId = 7, Text_Ar = "توفير خدمات طاقة مستدامة وبأسعار معقولة في مشاريع البنية التحتية", Text_En = "Providing affordable and sustainable energy services Energy supply in infrastructure projects", Weight = 0.134 },
            new SubCriterian { MainId = 7, Text_Ar = "استخدام التكنولوجيا الحديثة والمبتكرة في مشاريع البنية التحتية للطاقة", Text_En = "Using modern and innovative technology in energy infrastructure projects", Weight = 0.111 },
            new SubCriterian { MainId = 7, Text_Ar = "مساهمة مشاريع البنية التحتية للطاقة في تأثيرات تغير المناخ", Text_En = "Contribution of energy infrastructure projects to the effects of climate change", Weight = 0.255 },
            
            // Decent Work and Economic Growth (8)
            new SubCriterian { MainId = 8, Text_Ar = "عدد فرص العمل المباشرة وغير المباشرة التي توفرها مشاريع البنية التحتية", Text_En = "Number of direct and indirect job opportunities provided by infrastructure projects", Weight = 0.240 },
            new SubCriterian { MainId = 8, Text_Ar = "توفير عمل لائق من حيث الأجور والظروف والاحتياجات الاجتماعية في مشاريع البنية التحتية", Text_En = "Providing decent work in terms of wages, conditions and social needs in infrastructure projects", Weight = 0.179 },
            new SubCriterian { MainId = 8, Text_Ar = "مساهمة المشاريع في دمج الفئات المهنية اقتصادياً", Text_En = "Contribution of projects to the integration of professional groups Economically", Weight = 0.136 },
            new SubCriterian { MainId = 8, Text_Ar = "المساهمة في زيادة الإنتاجية والنمو الاقتصادي", Text_En = "Contributes to increasing economic productivity and growth", Weight = 0.096 },
            new SubCriterian { MainId = 8, Text_Ar = "استدامة مشاريع البنية التحتية من الناحية البيئية", Text_En = "Sustainability of infrastructure projects from an environmental", Weight = 0.350 },
            new SubCriterian { MainId = 8, Text_Ar = "توفير برامج الحماية الاجتماعية لعمال مشاريع البنية التحتية", Text_En = "Providing social protection programmes for workers of infrastructure projects", Weight = 0.053 },
            new SubCriterian { MainId = 8, Text_Ar = "تحقيق المساواة بين الجنسين في فرص العمل والأجور في مشاريع البنية التحتية", Text_En = "Achieving gender equality in employment opportunities and wages in infrastructure projects", Weight = 0.066 },
            
            // Industry, Innovation, and Infrastructure (9)
            new SubCriterian { MainId = 9, Text_Ar = "إمكانية الوصول إلى مشاريع البنية التحتية من قبل جميع شرائح المجتمع", Text_En = "Accessibility of infrastructure projects by all segments of society", Weight = 0.307 },
            new SubCriterian { MainId = 9, Text_Ar = "قدرة مشاريع البنية التحتية على التكيف مع التغيرات والمخاطر المستقبلية", Text_En = "The ability of infrastructure projects to adapt to future changes and risks", Weight = 0.186 },
            new SubCriterian { MainId = 9, Text_Ar = "مساهمة مشاريع البنية التحتية في تطوير الصناعات التحويلية وزيادة إنتاجيتها وتعزيز التنمية الصناعية", Text_En = "Infrastructure projects contribute to the development of transformation industries, increase their productivity and enhance industrial development", Weight = 0.122 },
            new SubCriterian { MainId = 9, Text_Ar = "دمج التكنولوجيا الحديثة والابتكارات في مشاريع البنية التحتية", Text_En = "Integration of modern technology and innovations in infrastructure projects", Weight = 0.155 },
            new SubCriterian { MainId = 9, Text_Ar = "مساهمة مشاريع البنية التحتية في الربط البيئي والتكامل الإقليمي", Text_En = "The contribution of infrastructure projects to environmental connectivity and regional integration", Weight = 0.230 },
            
            // Reduced Inequalities (10)
            new SubCriterian { MainId = 10, Text_Ar = "مساهمة مشاريع البنية التحتية في إشراك الفئات المهمشة اجتماعياً واقتصادياً", Text_En = "Contribution of infrastructure projects to the involvement of marginalized groups. Socially and economically", Weight = 0.164 },
            new SubCriterian { MainId = 10, Text_Ar = "إشراك وتمكين المجتمعات المحلية في مختلف مراحل مشاريع البنية التحتية", Text_En = "Involving and empowering local communities in various stages Infrastructure projects", Weight = 0.093 },
            new SubCriterian { MainId = 10, Text_Ar = "مساهمة مشاريع البنية التحتية في تقليل الفوارق التنموية بين المناطق المختلفة", Text_En = "Contribution of infrastructure projects to reducing development disparities between different regions", Weight = 0.069 },
            new SubCriterian { MainId = 10, Text_Ar = "توفير الخدمات الأساسية (الصحة-التعليم-الماء-الكهرباء) من خلال مشاريع البنية التحتية", Text_En = "Providing basic services (health-education-water-electricity) Through infrastructure projects", Weight = 0.332 },
            new SubCriterian { MainId = 10, Text_Ar = "توفير مشاريع البنية التحتية بأسعار معقولة لجميع شرائح المجتمع", Text_En = "Providing infrastructure projects at reasonable prices for all segments of society", Weight = 0.136 },
            new SubCriterian { MainId = 10, Text_Ar = "مراعاة مشاريع البنية التحتية للتنوع الثقافي والاحترام المتبادل بين المجتمعات", Text_En = "Infrastructure projects taking into account cultural diversity and mutual respect between communities.", Weight = 0.207 },
            
            // Sustainable Cities and Communities (11)
            new SubCriterian { MainId = 11, Text_Ar = "توفير النقل الكافي وإمكانية الوصول إلى مشاريع البنية التحتية", Text_En = "Providing adequate transportation and access to infrastructure projects", Weight = 0.323 },
            new SubCriterian { MainId = 11, Text_Ar = "مساهمة مشاريع البنية التحتية في تحقيق التكامل والتماسك الاجتماعي والحكومي", Text_En = "Contribution of infrastructure projects to social and governmental integration and cohesion", Weight = 0.223 },
            new SubCriterian { MainId = 11, Text_Ar = "إسهام مشاريع البنية التحتية في تحقيق الأمن والسلامة", Text_En = "Involvement of infrastructure projects in achieving security and safety", Weight = 0.118 },
            new SubCriterian { MainId = 11, Text_Ar = "قدرة المستخدمين على تحمل المخاطر والكوارث", Text_En = "Users' ability to withstand risks and disasters", Weight = 0.086 },
            new SubCriterian { MainId = 11, Text_Ar = "مساهمة مشاريع البنية التحتية وتقليل آثارها السلبية", Text_En = "Contribution of infrastructure projects and reducing their negative impacts", Weight = 0.046 },
            new SubCriterian { MainId = 11, Text_Ar = "التكامل والتخطيط الحضري المتكامل المستدام", Text_En = "Integration and sustainable integrated urban planning", Weight = 0.058 },
            new SubCriterian { MainId = 11, Text_Ar = "إشراك المجتمع في تخطيط وتنفيذ المشاريع", Text_En = "Involvement of the community in planning and implementing projects", Weight = 0.147 },
            
            // Responsible Consumption and Production (12)
            new SubCriterian { MainId = 12, Text_Ar = "استهلاك الموارد الطبيعية (الماء- الطاقة المتجددة-المواد الخام) في التصميم والتشغيل وتنفيذ المواد الصلبة", Text_En = "Consumption of natural resources (water- renewable energy-raw materials) in the design, operation and implementation of solid materials", Weight = 0.083 },
            new SubCriterian { MainId = 12, Text_Ar = "إعادة التدوير واستخدام النفايات الناتجة عن مشاريع البنية التحتية", Text_En = "Recycling and use of waste generated from infrastructure projects", Weight = 0.139 },
            new SubCriterian { MainId = 12, Text_Ar = "كفاءة الطاقة في مشاريع البنية التحتية", Text_En = "Energy efficiency in infrastructure projects", Weight = 0.102 },
            new SubCriterian { MainId = 12, Text_Ar = "تطبيق ممارسات الشراء المستدامة في مشاريع البنية التحتية", Text_En = "Application of sustainable procurement practices in infrastructure projects", Weight = 0.229 },
            new SubCriterian { MainId = 12, Text_Ar = "التقييم البيئي والاجتماعي للمشاريع قبل وبعد التنفيذ", Text_En = "Environmental and social assessment of projects before and after implementation", Weight = 0.277 },
            new SubCriterian { MainId = 12, Text_Ar = "تقييم مستوى الوعي والتدريب المقدم للعمال حول ممارسات الاستدامة في مشاريع البنية التحتية", Text_En = "Evaluation of the level of awareness and training provided to workers on sustainability practices in infrastructure projects", Weight = 0.170 },
            
            // Climate Action (13)
            new SubCriterian { MainId = 13, Text_Ar = "قياس مستويات غازات الدفيئة من مشاريع البنية التحتية", Text_En = "Measuring greenhouse gas levels from infrastructure projects", Weight = 0.323 },
            new SubCriterian { MainId = 13, Text_Ar = "قياس مستويات استهلاك الطاقة وكفاءتها في مشاريع البنية التحتية", Text_En = "Measuring energy consumption levels and efficiency in infrastructure projects", Weight = 0.223 },
            new SubCriterian { MainId = 13, Text_Ar = "استخدام الطاقة المتجددة في مشاريع البنية التحتية", Text_En = "Using renewable energy in infrastructure projects", Weight = 0.118 },
            new SubCriterian { MainId = 13, Text_Ar = "قدرة مشاريع البنية التحتية على التكيف مع تغير المناخ والأحداث المتطرفة", Text_En = "The ability of infrastructure projects to adapt to climate change and extreme events", Weight = 0.086 },
            new SubCriterian { MainId = 13, Text_Ar = "نسبة الاستثمار في التكيف مع المناخ", Text_En = "The percentage of investment in climate adaptation", Weight = 0.046 },
            new SubCriterian { MainId = 13, Text_Ar = "خطط الطوارئ والاستجابة لكوارث المناخ", Text_En = "Emergency plans and response to climate disasters", Weight = 0.058 },
            new SubCriterian { MainId = 13, Text_Ar = "مدى تحسين الأداء البيئي للبنية التحتية وتقليل الآثار السلبية على المناخ", Text_En = "The extent to which the environmental performance of infrastructure is improved and negative impacts on the climate are reduced", Weight = 0.147 },
            
            // Life Below Water (14)
            new SubCriterian { MainId = 14, Text_Ar = "قياس مستويات الملوثات البلاستيكية والكيميائية في الماء", Text_En = "Measuring levels of plastic and chemical pollutants in water", Weight = 0.301 },
            new SubCriterian { MainId = 14, Text_Ar = "التخلص من النفايات", Text_En = "Waste disposal", Weight = 0.441 },
            new SubCriterian { MainId = 14, Text_Ar = "دعم وتطوير تربية الأحياء المائية التي تساهم في الاقتصاد", Text_En = "Supporting and developing fish farming that contributes to the economy", Weight = 0.258 },
            
            // Life on Land (15)
            new SubCriterian { MainId = 15, Text_Ar = "مراعاة خطط استخدام الأراضي عند تصميم المشاريع", Text_En = "Consider land use plans when designing projects", Weight = 0.273 },
            new SubCriterian { MainId = 15, Text_Ar = "إدارة النفايات والمواد الخطرة الناتجة عن المشاريع لتجنب التلوث", Text_En = "Manage waste and hazardous materials generated by projects to avoid pollution", Weight = 0.188 },
            new SubCriterian { MainId = 15, Text_Ar = "تنفيذ برامج إعادة التشجير لتعويض أي خسائر في الغطاء النباتي والغابات", Text_En = "Implement reforestation programmes to compensate for any losses of forests and vegetation", Weight = 0.136 },
            new SubCriterian { MainId = 15, Text_Ar = "إشراك المجتمعات المحلية وأصحاب المصلحة في تنفيذ وتخطيط مشاريع البنية التحتية ذات التأثير على الأراضي", Text_En = "Involve local communities and stakeholders in the implementation and planning of infrastructure projects with land impact", Weight = 0.403 },
            
            // Peace, Justice, and Strong Institutions (16)
            new SubCriterian { MainId = 16, Text_Ar = "ضمان الامتثال لجميع القوانين واللوائح المعمول بها أثناء تنفيذ المشروع", Text_En = "Ensure compliance with all applicable laws and regulations during project implementation", Weight = 0.285 },
            new SubCriterian { MainId = 16, Text_Ar = "إنشاء آليات فعالة لتلقي الشكاوى والمظالم للأفراد المتأثرين بمشاريع البنية التحتية", Text_En = "Establish effective complaint and grievance mechanisms for individuals affected by infrastructure projects", Weight = 0.045 },
            new SubCriterian { MainId = 16, Text_Ar = "ضمان المعاملة العادلة والشفافة للمقاولين والمتعاقدين في عمليات الشراء والتعاقد", Text_En = "Ensure fair and transparent treatment of contractors and contractors in procurement and contracting processes", Weight = 0.029 },
            new SubCriterian { MainId = 16, Text_Ar = "تعزيز أنظمة المساءلة والرقابة على إنفاق الأموال العامة للمشاريع", Text_En = "Strengthen accountability and oversight systems for spending public funds for projects", Weight = 0.149 },
            new SubCriterian { MainId = 16, Text_Ar = "تدريب الموظفين على ممارسات النزاهة والشفافية في إدارة المشاريع", Text_En = "Train staff on integrity practices and transparency in project management", Weight = 0.020 },
            new SubCriterian { MainId = 16, Text_Ar = "ضمان مشاركة المجتمع في عمليات صنع القرار والرصد والتقييم", Text_En = "Ensure community participation in decision-making, monitoring and evaluation processes", Weight = 0.054 },
            new SubCriterian { MainId = 16, Text_Ar = "جعل المعلومات المتعلقة بالمشروع متاحة للجمهور بشكل منتظم وشفاف", Text_En = "Making project-related information available to the public on a regular and transparent basis", Weight = 0.016 },
            new SubCriterian { MainId = 16, Text_Ar = "ضمان احترام حقوق الملكية والتعويض العادل للأفراد المتأثرين", Text_En = "Ensure respect for property rights and fair compensation for affected individuals", Weight = 0.068 },
            new SubCriterian { MainId = 16, Text_Ar = "تعزيز التعاون بين الحكومات والمنظمات الدولية في مجال البنية التحتية", Text_En = "Enhance cooperation between governments and international organizations in the field of infrastructure", Weight = 0.108 },
            new SubCriterian { MainId = 16, Text_Ar = "تشجيع مشاركة القطاع الخاص والمجتمع المدني في تنفيذ المشاريع", Text_En = "Encourage the participation of the private sector and civil society in project implementation", Weight = 0.277 },
            
            // Partnerships for the Goal (17)
            new SubCriterian { MainId = 17, Text_Ar = "التزام الحكومة باستثمارات البنية التحتية كجزء من جهودها التنموية", Text_En = "Government commitment to infrastructure investments as part of its development efforts", Weight = 0.408 },
            new SubCriterian { MainId = 17, Text_Ar = "استثمارات القطاع الخاص في البنية التحتية", Text_En = "Private sector investments in infrastructure", Weight = 0.270 },
            new SubCriterian { MainId = 17, Text_Ar = "قدرة الحكومات المحلية على تطوير وتنفيذ السياسات التي تمكن من تنمية البنية التحتية الفعالة", Text_En = "Local government capacity to develop and implement policies that enable effective infrastructure development", Weight = 0.191 },
            new SubCriterian { MainId = 17, Text_Ar = "تطوير التكنولوجيا المناسبة للبنية التحتية المستدامة", Text_En = "Development of appropriate technology for sustainable infrastructure", Weight = 0.131 }
            };

            context.SubCriterians.AddRange(subCriteria);
            context.SaveChanges();
        }
    }
}
