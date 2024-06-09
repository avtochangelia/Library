**ინსტრუქციები გაშვებისთვის და მონაცემთა ბაზის კონფიგურაცია:**

1. Identity.Api და Admin.Api პროექტების აპსეთინგებში გაწერეთ თქვენი მონაცემთა ბაზის ქონექშენ სტრიგნი.

2. დასტარტეთ სამივე პროექტი ერთდროულად. Identity.Api პროექტის გაშვებისთანავე ავტომატიურად შეიქმენბა ბაზა და იუზერების ცხრილში გასიდული ადმინის ჩანაწერი:
<pre>
  "Username": "admin",
  "Password": "Admin123!"
</pre>


**ბიზნეს ლოგიკა:**

1. არაავტორიზებულ მომხმარებელს მხოლოდ შეუძლია იხილოს უკვე არსებული წიგნების და ავტორების სია.

2. რეგისტრაციის გავლის შემთხვევაში ავტომატიურად ენიჭება User როლი, რის შემდეგაც შეძლებს თავადაც დაამატოს ავტორები და წიგნები.

3. რეგისტრირებულ მომხმარებელს ასევე აქვს უფლება გაიტანოს ან დააბრუნოს წიგნები. (აქ ლოგიკას დიდად ყურადღება არ მივაქციე)

4. User როლის მქონე მომხმარებლებს შეუძლიათ მხოლოდ საკუთარი დამატებული ავტორის ან წიგნის წაშლა და რედაქტირება.

5. Admin როლს აქვს ნებისმიერ ჩანაწერის მართვის უფლება.


**პროექტის სტრუქტურა:**

1. ***Library.App*** არის ემვისი პროექტი, რომელიც იყენებს Identity.Api და Library.Api პროექტებში განთავსებულ ენდფოინთებს.

2. ***Identity.Api:*** პროექტი პასუხისმგებელია ავთენტიფიკაციისა და ავტორიზაციის ფუნქციების მართვაზე. პროექტში განთავსებულია მომხმარებლის რეგისტრაციის, ავტორიზაციის და ტოკენის მართვის ენდფოინთები.

3. ***Library.Api:*** პროექტი პასუხისმგებელია ძირითადი ფუნქციების მართვაზე. პროექტში განთავსებულია მომხმარებლების, წიგნების და ავტორების მართვის ენდფოინთები.

4. ***Application:*** ლეიერში განთავსებულია აპლიკაციის ბიზნეს ლოგიკა.

3. ***DI:*** ლეიერში ხდება დეფენდენსი ინჯექშენის კონფიგურაცია და საერთო სერვისების რეგისტრაცია.

4. ***Domain:*** ლეიერი აერთიანებს ძირითად დომეინებს და ბიზნეს ლოგიკას.

5. ***Infrastructure:*** ლეიერი უზრუნველყოფს ბაზასთან კომუნიკაციას.

6. ***Shared:*** ლეიერში განთავსებულია საერთო ინამები, კლასები, მეთოდები და ასე შემდეგ.
