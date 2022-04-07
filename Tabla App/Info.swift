//
//  Info.swift
//  Tabla App
//
//  Created by Swagnik Roychoudhury on 9/7/21.
//

import SwiftUI

struct About: View {
    
    @StateObject var modifiers = ScreenModifiers()
    
    var body: some View {
        
        
        
        ZStack {

            Color.black
                .ignoresSafeArea()
            
            Image("Ghungroo2").centerCropped()
            
            VStack{
                
                Spacer()
                    .frame(height: modifiers.ht * 50)
                
                ScrollView{
                    
                    Spacer()
                        .frame(height: modifiers.ht * 50)
                    
                    Text("Kathak Saangi")
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 50))
                        .foregroundColor(.gold2)
                    
                    
                    Text("Kathak Saangi is the perfect companion for kathak dancers. Every module in this app offers a unique benefit to dancers. Thoughtfully crafted, it will be very useful for their daily practice routine - a must have app for the drills with the different levels of layakari and recitations. Kathak dancers will study important aspects and elements of Taals, Lari, Upaj and more. This app is an effective tool in understanding the complex calculations of different types of Tihais and it will help dancers to create their own Tihais. Guru Sandip Mallick ji demonstrates a very productive sessions on the geometric concept of kathak as well as different types of footwork. At the same time, dancers will explore contemporary octopad compositions for custom choreographies.")
                        .font(.system(size: modifiers.wt * 16))
                        .foregroundColor(.gold2)
                        .padding(.horizontal, modifiers.ht * 50)
                        .padding(.vertical, modifiers.ht * 20)
                        .fixedSize(horizontal: false, vertical: true)
                        .multilineTextAlignment(.leading)
                    

                    
                    Spacer()
                    
                }
                .frame(width: modifiers.wt * 350, height: modifiers.ht * 620)
                .background(Color.black2)
                .cornerRadius(modifiers.wt * 40.0)
                
                
            }
    
        }

        
        
        
    }
}


struct Credits: View {
    
    @StateObject var modifiers = ScreenModifiers()
    
    @State var showSMAbout = false
    @State var showAMAbout = false
    @State var showSRAbout = false
    @State var showSM2About = false
    
    
    var SMabout = ["Sandip Mallick,  an internationally acclaimed Kathak exponent,  started training under Guru Smt.  Sreelekha Mukherjee at the tender age of four. Continuing the talim under her he also has got associated with Pt.Birju Maharaj ji,Pt. Chitreash Das, Guru Bela Arnab, Guru Bandana Sen,Pt. Ram mohan Maharaj,Pt. Vijay Shankar,Smt. Saswati Sen. He has undergone his rewaz (practice) with Pt. Kumar Bose on tabla since 1997.", "An M.A from Rabindra Bharati University, Sandip  is an auditioned artiste of Doordarshan and empanelled artiste of ICCR. He is a Junior fellowship holder of Govt of India. His topic was \" Geometric analysis of basic movements and footwork used in Kathak\". He is a regular performer in various prestigious festival like Khajuraho, Chidambarahm,  Konark, Kathak Mahotsav, SNA Swarna Samaraho , Udayshankar and many more in India and in USA, Canada, UK,  Germany, Spain, Czech Republic, Italy, Singapore ,Dubai and Bangladesh.", "He worked at Padatik over a decade as a Guru and in The Heritage school as a kathak instructor. As a mentor he is associated with Tarana Dance academy ,NJ. Upaz in Dubai,Damru in Czech Republic and many more in India. Sandip is the founder director of Sonarpur Nadam .The students of Nadam are working across the globe with a remarkable reputation.", "Sandip Says \" Dance is a language of communication and kathak is my mother tongue\""]
    
    var AMabout = ["Born in a musically dedicated family, Aniruddha is one of the most talented and flourishing Tabla players .From the very early age he started receiving his talim from Pt.Debashish Mukherjee.He has the forutune to have talim from leading Tabla exponent  Pt. Shubhankar  Banerjee.","Aniruddha got his Master's Degree (in Music) from the Rabindra Bharati University.He has bagged several prizes and honours for his brilliant performances.Some of his prizes and honours include 2nd position in the All India Radio Music Competition in 1999, First in Ganguly College of Music Competition, Kolkata in 1994, got First prize from All Bengal Music Competition in 1994. Even he managed to get a position from the W.B State Music Academy, Kolkata.","Aniruddha has the distinction of participating in a number of music conferences in India like Naveen Prativa Sammelan , State Music Academy in 1997, ICCR in 1998, All India Radio, Patna in 2000, India International Centre in 2001, Spirit of Unity Concert, Vizag (Andhra Pradesh)2001,Haridas Sangeet Sammelon, Mumbai 2006, Brighton Festival, UK Bharatiya Vidya  Bhavan, London,UK, Music Gimeete, Paris France, Juanju Festival, Harbour front Festival- Canada, USA, Seoul South  Korea Asia Festival 2008 ,Tokio Japan India Festival, Budapest, Austria 2009,Shankot Mochon Festival,Beneras,2009,Sur and Harmony Festival, New Delhi,2009.He also performed in Italy, Croatia, West Germany, Hungary and Vietnam under I.C.C.R. Tour.","He is very fortunate to accompany Pt. Buddhadev Dasgupta, Vidushi Sumitra Guha, Parvin Sultana, Pt. Birju Maharaj, Pt. Pratap Pawar,  Eminent Sitar Maestro Pt. Shyamal Chatterjee, Dr. N.Rajan, Shri Gaurav Majumdar, Shri Shuvendra Rao, Pt. Rajendra Gangani, Vidushi Shaswati Sen, Smt Vaswati Sen, Shri Sandip Mallick Of late he accompanied with Anushka Shankar at House of  Commons, London.  Presently he is a graded artist of All India Radio, TV, Kolkata India."]
    
    var SRabout = ["Swagnik Roychoudhury is a 12th grader at the Middlesex County Academy for Science, Mathematics and Engineering Technologies - a magnet high school in Edison, New Jersey.", "His main focus is Computer Science and Mathematics - he has done multiple computer science projects (this Kathak App being one of the biggest) and takes advanced calculus and AI courses from Stanford University. His next focus will be AI research in the subject of Chess.", "He has completed his B.A degree in Tabla from Sarbabharatiya Sangeet-O-Sanskriti Parishad of Kolkata. He is a student of Guru Aniruddha Mukherjee and has been learning Tabla for 11 years. ","Along with music, he is also very passionate about chess. He is an USCF rated chess player with USCF rating of 1800."]
    
    var SM2about = ["Shuvom Mallick is a 6th grader and lives in Kolkata. He is the son of Guru Sandip Mallick, from whom he is getting his taalim of Kathak from a very tender age","Shuvom has participated in numerous successful events in Kolkata, at his school and at local programs","He is a young and talented kathak dancer with a bright and promising future."]
    
    var body: some View {
        
        ZStack {

            Color.black
                .ignoresSafeArea()
            
            Image("Ghungroo2").centerCropped()
            
            VStack{
                
                Spacer()
                    .frame(height: modifiers.ht * 70)
                
                VStack{
                    
                    ScrollView{
                        
                        Spacer()
                            .frame(height: modifiers.ht * 20)
                        
                        Text("Credits")
                            .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 50))
                            .foregroundColor(.gold2)
                            .frame(width: modifiers.wt * 320, height: modifiers.ht * 84 * 0.90)
                            .border(width: modifiers.ht * 2, edges:[.bottom], color: .gold2)
                        
                        VStack{
                            Spacer()
                            
                            HStack{
                                Spacer()
                                Text("Guru Sandip Mallick - Demonstrations, Lectures")
                                    .font(.system(size: modifiers.wt * 17))
                                    .foregroundColor(.gold2)
                                    .fixedSize(horizontal: false, vertical: true)
                                    .multilineTextAlignment(.center)
                                Spacer()
                            }
                            
                            HStack{
                                
                                VStack{
                                    Text(SMabout[0])
                                        .frame(width: modifiers.wt * 280, height: modifiers.ht * 80)
                                        .font(.system(size: modifiers.wt * 15))
                                        .foregroundColor(.gold2)
                                    
                                    Spacer()
                                    
                                    Button(action: {
                                        
                                        self.showSMAbout.toggle()
                                        
                                        
                                    }, label: {
                                        Text("Read More")
                                            .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 13))
                                            .foregroundColor(.blue)
                                            .underline()
                                    })
                                }
                            }
                            .frame(width: modifiers.wt * 320, height: modifiers.ht * 90)
                            .padding(.vertical)
                            .border(width: 2, edges:[.bottom], color: .gold2)
                        }
                        
                        
                        VStack{
                            Spacer()
                            
                            HStack{
                                Spacer()
                                Text("Guru Aniruddha Mukherjee - Tabla Teacher")
                                    .font(.system(size: modifiers.wt * 17))
                                    .foregroundColor(.gold2)
                                    .fixedSize(horizontal: false, vertical: true)
                                    .multilineTextAlignment(.center)
                                Spacer()
                            }
                            
                            HStack{
                                
                                VStack{
                                    Text(AMabout[0])
                                        .frame(width: modifiers.wt * 280, height: modifiers.ht * 80)
                                        .font(.system(size: modifiers.wt * 15))
                                        .foregroundColor(.gold2)
                                    
                                    Spacer()
                                    
                                    Button(action: {
                                        
                                        self.showAMAbout.toggle()
                                        
                                        
                                    }, label: {
                                        Text("Read More")
                                            .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 13))
                                            .foregroundColor(.blue)
                                            .underline()
                                    })
                                }
                            }
                            .frame(width: modifiers.wt * 320, height: modifiers.ht * 90)
                            .padding(.vertical)
                            .border(width: 2, edges:[.bottom], color: .gold2)
                        }
                        
                        
                        VStack{
                            Spacer()
                            
                            HStack{
                                Spacer()
                                Text("Swagnik Roychoudhury - App Concept, App Development, App Design, Tabla, Recitation, Lectures")
                                    .font(.system(size: modifiers.wt * 17))
                                    .foregroundColor(.gold2)
                                    .fixedSize(horizontal: false, vertical: true)
                                    .multilineTextAlignment(.center)
                                Spacer()
                            }
                            
                            HStack{
                                
                                VStack{
                                    Text(SRabout[0])
                                        .frame(width: modifiers.wt * 280, height: modifiers.ht * 80)
                                        .font(.system(size: modifiers.wt * 15))
                                        .foregroundColor(.gold2)
                                    
                                    Spacer()
                                    
                                    Button(action: {
                                        
                                        self.showSRAbout.toggle()
                                        
                                        
                                    }, label: {
                                        Text("Read More")
                                            .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 13))
                                            .foregroundColor(.blue)
                                            .underline()
                                    })
                                }
                            }
                            .frame(width: modifiers.wt * 320, height: modifiers.ht * 90)
                            .padding(.vertical)
                            .border(width: 2, edges:[.bottom], color: .gold2)
                        }
                        
                        
                        VStack{
                            Spacer()
                            
                            HStack{
                                Spacer()
                                Text("Shuvom Mallick - Demonstrator")
                                    .font(.system(size: modifiers.wt * 17))
                                    .foregroundColor(.gold2)
                                    .fixedSize(horizontal: false, vertical: true)
                                    .multilineTextAlignment(.center)
                                Spacer()
                            }
                            
                            HStack{
                                
                                VStack{
                                    Text(SM2about[0])
                                        .frame(width: modifiers.wt * 280, height: modifiers.ht * 80)
                                        .font(.system(size: modifiers.wt * 15))
                                        .foregroundColor(.gold2)
                                    
                                    Spacer()
                                    
                                    Button(action: {
                                        
                                        self.showSM2About.toggle()
                                        
                                        
                                    }, label: {
                                        Text("Read More")
                                            .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 13))
                                            .foregroundColor(.blue)
                                            .underline()
                                    })
                                }
                            }
                            .frame(width: modifiers.wt * 320, height: modifiers.ht * 90)
                            .padding(.vertical)
                            .border(width: 2, edges:[.bottom], color: .gold2)
                        }
                        
                        
                        Spacer()
                        
                    }
                    
                }
                .frame(width: modifiers.wt * 360, height: modifiers.ht * 844 * 0.80)
                .background(Color.black2)
                .cornerRadius(modifiers.wt * 40.0)
                
            }
            
            
            if self.showSMAbout == true{
                
                ZStack{
                    
                    VStack{
                        
                        Spacer()
                            .frame(height: modifiers.ht * 70)
                        ScrollView {
                            
                            
                            Spacer()
                                .frame(height: modifiers.ht * 50)
                            
                            Text(SMabout[0])
                                .frame(width: modifiers.wt * 300)
                                .font(.system(size: modifiers.wt * 15))
                                .foregroundColor(.gold2)
                                .fixedSize(horizontal: false, vertical: true)
                            
                            Spacer()
                                .frame(height: modifiers.ht * 20)
                            
                            Text(SMabout[1])
                                .frame(width: modifiers.wt * 300)
                                .font(.system(size: modifiers.wt * 15))
                                .foregroundColor(.gold2)
                                .fixedSize(horizontal: false, vertical: true)
                            
                            Spacer()
                                .frame(height: modifiers.ht * 20)
                            
                            Text(SMabout[2])
                                .frame(width: modifiers.wt * 300)
                                .font(.system(size: modifiers.wt * 15))
                                .foregroundColor(.gold2)
                                .fixedSize(horizontal: false, vertical: true)
                            
                            Spacer()
                                .frame(height: modifiers.ht * 20)
                            
                            Text(SMabout[3])
                                .frame(width: modifiers.wt * 300)
                                .font(.system(size: modifiers.wt * 15))
                                .foregroundColor(.gold2)
                                .fixedSize(horizontal: false, vertical: true)
                            
                            
                        }
                        .frame(width: modifiers.wt * 360, height: modifiers.ht * 844 * 0.80)
                        .background(Color.black2)
                        .cornerRadius(modifiers.wt * 40.0)
                        
                    }
                    
                    VStack{
                        
                        Spacer()
                            .frame(height: modifiers.ht * 110)
                        
                        HStack{
                            
                            Spacer()
                                .frame(width: modifiers.wt * 290)
                            
                            Button(action: {
                                
                                self.showSMAbout.toggle()
                                
                                
                            }, label: {
                                Image(systemName: "xmark")
                                    .resizable()
                                    .foregroundColor(.gold2)
                                    .frame(width: modifiers.ht * 15, height: modifiers.ht * 15)
                            })
                        }
                        
                        Spacer()
                    }
                    
                }
            }
            
            if self.showAMAbout == true{
                
                ZStack{
                      
                    VStack{
                        
                        Spacer()
                            .frame(height: modifiers.ht * 70)
                    
                    ScrollView {
                        
                        
                        Spacer()
                            .frame(height: modifiers.ht * 50)
                        
                        Text(AMabout[0])
                            .frame(width: modifiers.wt * 300)
                            .font(.system(size: modifiers.wt * 15))
                            .foregroundColor(.gold2)
                            .fixedSize(horizontal: false, vertical: true)
                        
                        Spacer()
                            .frame(height: modifiers.ht * 20)
                        
                        Text(AMabout[1])
                            .frame(width: modifiers.wt * 300)
                            .font(.system(size: modifiers.wt * 15))
                            .foregroundColor(.gold2)
                            .fixedSize(horizontal: false, vertical: true)
                        
                        Spacer()
                            .frame(height: modifiers.ht * 20)
                        
                        Text(AMabout[2])
                            .frame(width: modifiers.wt * 300)
                            .font(.system(size: modifiers.wt * 15))
                            .foregroundColor(.gold2)
                            .fixedSize(horizontal: false, vertical: true)
                        
                        Spacer()
                            .frame(height: modifiers.ht * 20)
                        
                        Text(AMabout[3])
                            .frame(width: modifiers.wt * 300)
                            .font(.system(size: modifiers.wt * 15))
                            .foregroundColor(.gold2)
                            .fixedSize(horizontal: false, vertical: true)
                        
                        
                    }
                    .frame(width: modifiers.wt * 360, height: modifiers.ht * 844 * 0.80)
                    .background(Color.black2)
                    .cornerRadius(modifiers.wt * 40.0)
                        
                    }
                    
                    VStack{
                        
                        Spacer()
                            .frame(height: modifiers.ht * 110)
                        
                        HStack{
                            
                            Spacer()
                                .frame(width: modifiers.wt * 290)
                            
                            Button(action: {
                                
                                self.showAMAbout.toggle()
                                
                                
                            }, label: {
                                Image(systemName: "xmark")
                                    .resizable()
                                    .foregroundColor(.gold2)
                                    .frame(width: modifiers.ht * 15, height: modifiers.ht * 15)
                            })
                        }
                        
                        Spacer()
                    }
                    
                }
            }
            
            if self.showSRAbout == true{
                
                ZStack{
                    
                    VStack{
                        
                        Spacer()
                            .frame(height: modifiers.ht * 70)
                        ScrollView {
                            
                            
                            Spacer()
                                .frame(height: modifiers.ht * 50)
                            
                            Text(SRabout[0])
                                .frame(width: modifiers.wt * 300)
                                .font(.system(size: modifiers.wt * 15))
                                .foregroundColor(.gold2)
                                .fixedSize(horizontal: false, vertical: true)
                            
                            Spacer()
                                .frame(height: modifiers.ht * 20)
                            
                            Text(SRabout[1])
                                .frame(width: modifiers.wt * 300)
                                .font(.system(size: modifiers.wt * 15))
                                .foregroundColor(.gold2)
                                .fixedSize(horizontal: false, vertical: true)
                            
                            Spacer()
                                .frame(height: modifiers.ht * 20)
                            
                            Text(SRabout[2])
                                .frame(width: modifiers.wt * 300)
                                .font(.system(size: modifiers.wt * 15))
                                .foregroundColor(.gold2)
                                .fixedSize(horizontal: false, vertical: true)
                            
                            Spacer()
                                .frame(height: modifiers.ht * 20)
                            
                            Text(SRabout[3])
                                .frame(width: modifiers.wt * 300)
                                .font(.system(size: modifiers.wt * 15))
                                .foregroundColor(.gold2)
                                .fixedSize(horizontal: false, vertical: true)
                            
                            
                        }
                        .frame(width: modifiers.wt * 360, height: modifiers.ht * 844 * 0.80)
                        .background(Color.black2)
                        .cornerRadius(modifiers.wt * 40.0)
                        
                    }
                    
                    VStack{
                        
                        Spacer()
                            .frame(height: modifiers.ht * 110)
                        
                        HStack{
                            
                            Spacer()
                                .frame(width: modifiers.wt * 290)
                            
                            Button(action: {
                                
                                self.showSRAbout.toggle()
                                
                                
                            }, label: {
                                Image(systemName: "xmark")
                                    .resizable()
                                    .foregroundColor(.gold2)
                                    .frame(width: modifiers.ht * 15, height: modifiers.ht * 15)
                            })
                        }
                        
                        Spacer()
                    }
                    
                }
            }
            
            if self.showSM2About == true{
                
                ZStack{
                    
                    VStack{
                        
                        Spacer()
                            .frame(height: modifiers.ht * 70)
                        ScrollView {
                            
                            Spacer()
                                .frame(height: modifiers.ht * 50)
                            
                            Text(SM2about[0])
                                .frame(width: modifiers.wt * 300)
                                .font(.system(size: modifiers.wt * 15))
                                .foregroundColor(.gold2)
                                .fixedSize(horizontal: false, vertical: true)
                            
                            Spacer()
                                .frame(height: modifiers.ht * 20)
                            
                            Text(SM2about[1])
                                .frame(width: modifiers.wt * 300)
                                .font(.system(size: modifiers.wt * 15))
                                .foregroundColor(.gold2)
                                .fixedSize(horizontal: false, vertical: true)
                            
                            Spacer()
                                .frame(height: modifiers.ht * 20)
                            
                            Text(SM2about[2])
                                .frame(width: modifiers.wt * 300)
                                .font(.system(size: modifiers.wt * 15))
                                .foregroundColor(.gold2)
                                .fixedSize(horizontal: false, vertical: true)
                            
                        }
                        .frame(width: modifiers.wt * 360, height: modifiers.ht * 844 * 0.80)
                        .background(Color.black2)
                        .cornerRadius(modifiers.wt * 40.0)
                        
                    }
                    
                    VStack{
                        
                        Spacer()
                            .frame(height: modifiers.ht * 110)
                        
                        HStack{
                            
                            Spacer()
                                .frame(width: modifiers.wt * 290)
                            
                            Button(action: {
                                
                                self.showSM2About.toggle()
                                
                                
                            }, label: {
                                Image(systemName: "xmark")
                                    .resizable()
                                    .foregroundColor(.gold2)
                                    .frame(width: modifiers.ht * 15, height: modifiers.ht * 15)
                            })
                        }
                        
                        Spacer()
                    }
                    
                }
            }
    
        }

        
        
        
    }
}


struct Contact: View {
    
    @StateObject var modifiers = ScreenModifiers()
    
    var body: some View {
        
        
        
        ZStack {

            Color.black
                .ignoresSafeArea()
            
            Image("Ghungroo2").centerCropped()
            
            VStack{
                
                Spacer()
                    .frame(height: modifiers.ht * 50)
                
                VStack{
                    
                    Spacer()
                        .frame(height: modifiers.ht * 50)
                    
                    Text("Contact Us")
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 50))
                        .foregroundColor(.gold2)
                    Spacer()
                    
                    Text("Email")
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 35))
                        .foregroundColor(.gold2)
                        .padding(.horizontal, modifiers.ht * 50)
                        .padding(.vertical, modifiers.ht * 5)
                    
                    Text("kathaksaangi@gmail.com")
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 25))
                        .foregroundColor(.gold2)
                        .padding(.horizontal, modifiers.ht * 20)
                        .padding(.vertical, modifiers.ht * 10)
                    
                    Spacer()
                    
                    
                    Text("Phone Number")
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 35))
                        .foregroundColor(.gold2)
                        .padding(.horizontal, modifiers.ht * 50)
                        .padding(.vertical, modifiers.ht * 10)
                    Text("+1 (609)-509-1289")
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 30))
                        .foregroundColor(.gold2)
                        .padding(.horizontal, modifiers.ht * 50)
                        .padding(.vertical, modifiers.ht * 5)
                    Text("+1 (732) - 322 - 8963")
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 30))
                        .foregroundColor(.gold2)
                        .padding(.horizontal, modifiers.ht * 50)
                        .padding(.vertical, modifiers.ht * 5)

                    
                    Spacer()
                    
                }
                .frame(width: modifiers.wt * 350, height: modifiers.ht * 590)
                .background(Color.black2)
                .cornerRadius(modifiers.wt * 40.0)
                
                
            }
    
        }

        
        
        
    }
}


struct PrivacyPolicy: View {
    
    @StateObject var modifiers = ScreenModifiers()
    
    var body: some View {
        
        
        
        ZStack {

            Color.black
                .ignoresSafeArea()
            
            Image("Ghungroo2").centerCropped()
            
            VStack{
                
                Spacer()
                    .frame(height: modifiers.ht * 50)
                
                VStack{
                    
                    Spacer()
                        .frame(height: modifiers.ht * 50)
                    
                    Text("Privacy Policy")
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 50))
                        .foregroundColor(.gold2)
                    
                    Text("We Do Not Collect Any Information At Any Time")
                        .multilineTextAlignment(.center)
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 18))
                        .foregroundColor(.gold2)
                        .padding(modifiers.ht * 30)
                    
                    Text("We do not collect or have access to your data. We do not store user preferences or settings, as in this app, there are none. We do not have access to servers nor do we have access to your files and photos to store your information.  ")
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 16))
                        .foregroundColor(.gold2)
                        .padding(.horizontal, modifiers.ht * 50)
                        .padding(.vertical, modifiers.ht * 20)
                    
                    Text("While we use 3rd party applications like YouTube to host audio and video, we do not send information to YouTube as we have no way to access any information in the first place. For more information on YouTube:")
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 16))
                        .foregroundColor(.gold2)
                        .padding(.horizontal, modifiers.ht * 50)
                        .padding(.vertical, modifiers.ht * 20)
                    
                    Link(destination: URL(string: "https://policies.google.com/privacy?hl=en")!){
                        Text("YouTube's Privacy Policy")
                            .foregroundColor(.blue)
                            .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 18))
                    }
                    
                    Spacer()
                    
                }
                .frame(width: modifiers.wt * 350, height: modifiers.ht * 590)
                .background(Color.black2)
                .cornerRadius(modifiers.wt * 40.0)
                
                
            }
    
        }

        
        
        
    }
}


struct Info_Previews: PreviewProvider {
    static var previews: some View {
        Contact()
    }
}





