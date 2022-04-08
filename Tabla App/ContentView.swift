//

//  ContentView.swift

//  Tabla

//

//  Created by Swagnik Roychoudhury on 3/14/21.

//



import SwiftUI
import AVKit
import Foundation


struct ContentView_Previews: PreviewProvider {
    
    static var previews: some View {
        
        Group {
            ContentView().preferredColorScheme(.dark)
            
            
        }
        
    }
    
}

struct ContentView: View {
    
    var body: some View {
       
        NavigationView{
            
            Splash()
            .navigationBarTitle("")
            .navigationBarHidden(true)
            
            //Home()
        }.navigationViewStyle(StackNavigationViewStyle())
        
    }
    
}

struct Splash: View {
    
    @StateObject var modifiers = ScreenModifiers()
    
    var body: some View{
        

        VStack{
        
            VStack{

                Spacer()

                Text("Kathak Saangi")
                    .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 55))
                    .foregroundColor(.gold2)
                    .frame(width: modifiers.wt * 360, height: modifiers.ht * 100, alignment: .center)
                    //.background(Color.gold1)
                    .cornerRadius(modifiers.wt * 30.0)
                
                
                Text("The Ultimate")
                    .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 30))
                    .foregroundColor(.gold2)
                    .frame(width: modifiers.wt * 360, height: modifiers.ht * 40, alignment: .center)
                    .fixedSize(horizontal: false, vertical: true)
                    .multilineTextAlignment(.center)
                    //.background(Color.gold1)
                    .cornerRadius(modifiers.wt * 30.0)
                
                Text("Kathakar's Companion")
                    .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 30))
                    .foregroundColor(.gold2)
                    .frame(width: modifiers.wt * 360, height: modifiers.ht * 40, alignment: .center)
                    .fixedSize(horizontal: false, vertical: true)
                    .multilineTextAlignment(.center)
                    //.background(Color.gold1)
                    .cornerRadius(modifiers.wt * 30.0)


                Spacer()
                
                Image("Dancer")
                    .resizable()
                    //.aspectRatio(contentMode: .fill)
                    .frame(width: modifiers.wt * 300, height: modifiers.wt * 320, alignment: .center)
                    .background(Color.gold1)
                    .cornerRadius(modifiers.wt * 30.0)

                
                Spacer()

                NavigationLink(destination: TestView()){

                    Text("Enter")
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 40))
                        .foregroundColor(.black)
                        .frame(width: modifiers.wt * 200, height: modifiers.ht * 70, alignment: .center)
                        .background(Color.gold1)
                        .cornerRadius(modifiers.wt * 30.0)

                }.buttonStyle(ThemeAnimationStyle(splash: true))


                Spacer()

            }
            
            
            Spacer()
        }.background(
            Image("TB2")
                .resizable()
                .opacity(1)
                .aspectRatio(contentMode: .fill)
            
            
        )
            

    }
}


struct Home: View {
    
    @StateObject var modifiers = ScreenModifiers()

    var spacing: CGFloat = 20
    
    @State var showInfo = false
    
    
    init() {
        UINavigationBar.appearance().titleTextAttributes = [.foregroundColor: UIColor(named: "gold2") ?? .white]
    }

    var body: some View {
        
        ZStack {

            Color.black
                .ignoresSafeArea()
            
            Image("Ghungroo4").centerCropped()
            
            LinearGradient(gradient: Gradient(colors: [Color.clear, Color.red1 ,Color.clear]), startPoint: .leading, endPoint: .trailing).ignoresSafeArea()
            
            

            VStack{
                
                //Spacer()
                    //.frame(height: 100)
                
                Group{
                    Group
                    {
                        Spacer()
                            .frame(height: 15)
                        
                    }
                    
                    Group{
                        Group
                        {
                            
                            NavigationLink(destination: Riyaz()){
                                
                                Text("Riyaz")
                                    .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 25))
                                    .fontWeight(/*@START_MENU_TOKEN@*/.bold/*@END_MENU_TOKEN@*/)
                                    .padding()
                                    .foregroundColor(.gold2)
                                    .background(Color.clear)
                            }.buttonStyle(ThemeAnimationStyle(splash: false))
                            
                        }
                        
                        Group
                        {
                            Spacer()
                                .frame(height: modifiers.ht * spacing)
                            
                            NavigationLink(destination: Kramalaya()){
                                
                                Text("Kramalaya")
                                    .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 25))
                                    .fontWeight(/*@START_MENU_TOKEN@*/.bold/*@END_MENU_TOKEN@*/)
                                    .padding()
                                    .foregroundColor(.gold2)
                                    .background(Color.clear)
                            }.buttonStyle(ThemeAnimationStyle(splash: false))
                        }
                        
                        Group
                        {
                            Spacer()
                                .frame(height: modifiers.ht * spacing)
                            
                            NavigationLink(destination: Ladi_Upaj()){
                                
                                Text("Ladi and Upaj")
                                    .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 25))
                                    .fontWeight(/*@START_MENU_TOKEN@*/.bold/*@END_MENU_TOKEN@*/)
                                    .padding()
                                    .foregroundColor(.gold2)
                                    .background(Color.clear)
                                
                            }.buttonStyle(ThemeAnimationStyle(splash: false))
                        }
                        
                        Group
                        {
                            Spacer()
                                .frame(height: modifiers.ht * spacing)

                            NavigationLink(destination: DasaPrana()){
                                
                                Text("Dasa Prana")
                                    .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 25))
                                    .padding()
                                    .foregroundColor(.gold2)
                                    .background(Color.clear)
                                    .fixedSize(horizontal: false, vertical: true)
                            }.buttonStyle(ThemeAnimationStyle(splash: false))
                        }
                        
                        Group
                        {
                            Spacer()
                                .frame(height: modifiers.ht * spacing)
                            
                            NavigationLink(destination: Tehai()){
                                
                                Text("Tehai")
                                    .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 25))
                                    .fontWeight(/*@START_MENU_TOKEN@*/.bold/*@END_MENU_TOKEN@*/)
                                    .padding()
                                    .foregroundColor(.gold2)
                                    .background(Color.clear)
                                
                            }.buttonStyle(ThemeAnimationStyle(splash: false))
                            
                        }
                        
                        Group
                        {
                            Spacer()
                                .frame(height: modifiers.ht * spacing)
                            
                            NavigationLink(destination: TestView()){
                                
                                Text("Tabla Player")
                                    .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 25))
                                    .fontWeight(/*@START_MENU_TOKEN@*/.bold/*@END_MENU_TOKEN@*/)
                                    .padding()
                                    .foregroundColor(.gold2)
                                    .background(Color.clear)
                                
                            }.buttonStyle(ThemeAnimationStyle(splash: false))
                            
                        }
                        
                        Group
                        {
                            Spacer()
                                .frame(height: modifiers.ht * spacing)
                            
                            NavigationLink(destination: Lehra()){
                                
                                Text("Teentaal Lehra")
                                    .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 25))
                                    .fontWeight(/*@START_MENU_TOKEN@*/.bold/*@END_MENU_TOKEN@*/)
                                    .padding()
                                    .foregroundColor(.gold2)
                                    .background(Color.clear)
                                
                            }.buttonStyle(ThemeAnimationStyle(splash: false))
                            
                        }
                        
                        Group
                        {
                            Spacer()
                                .frame(height: modifiers.ht * spacing)
                            
                            NavigationLink(destination: Octapad()){
                                
                                Text("Octapad")
                                    .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 25))
                                    .padding()
                                    .foregroundColor(.gold2)
                                    .background(Color.clear)
                                
                            }.buttonStyle(ThemeAnimationStyle(splash: false))
                        }
                        
                        Group
                        {
                            Spacer()
                                .frame(height: modifiers.ht * spacing)
                            
                            NavigationLink(destination: Padhant()){
                                
                                Text("Padhant")
                                    .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 25))
                                    .padding()
                                    .foregroundColor(.gold2)
                                    .background(Color.clear)
                                
                            }.buttonStyle(ThemeAnimationStyle(splash: false))
                        }
                        
                        Group
                        {
                            Spacer()
                                .frame(height: modifiers.ht * spacing)
                            
                            NavigationLink(destination: Gurus()){
                                
                                Text("Our Gurus")
                                    .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 25))
                                    .padding()
                                    .foregroundColor(.gold2)
                                    .background(Color.clear)
                                    .fixedSize(horizontal: false, vertical: true)
                                    .multilineTextAlignment(.center)

                            }.buttonStyle(ThemeAnimationStyle(splash: false))
                        }
                        
                        
                    }
                    
                    Group
                    {
                        Spacer()
                            .frame(height: modifiers.ht * spacing)
                    }
                }
            }
            .navigationBarBackButtonHidden(true).navigationBarTitle("Home", displayMode: .inline).navigationBarHidden(true)
            .frame(width: modifiers.wt * 350, height: modifiers.ht * 844 * 0.90)
            .background(Color.black1)
            .cornerRadius(modifiers.wt * 40.0)

            
            VStack{
                
                Spacer()
                    .frame(height: modifiers.ht * 40)
                
                
                HStack{
                    Spacer()
                        .frame(width: modifiers.wt * 265)
                    
                    Button(action: {
                        withAnimation(.spring()){
                            self.showInfo.toggle()
                        }
                    }
                    ) {
                        Image(systemName: self.showInfo ? "xmark" : "info.circle")
                            .resizable()
                            .foregroundColor(.gold2)
                            .frame(width: modifiers.ht * 25, height: modifiers.ht * 25)
                    }
                }
                
                Spacer()
                    .frame(height: modifiers.ht * 15)
                
                HStack{
                    
                    Spacer()
                        .frame(width: modifiers.wt * 230)
                    
                    if self.showInfo{
                        InfoPopUp()
                    }
                }

                
                Spacer()
            
            }
            .padding()
        }
        .onAppear{
            showInfo.self = false
        }
    }
}


struct InfoPopUp : View {
    
    @StateObject var modifiers = ScreenModifiers()
    
    var body : some View{
        
        VStack(alignment: .leading, spacing: modifiers.ht * 5) {
            
            NavigationLink(destination: About()){
                
                HStack{
                    Text("About")
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 12))
                }
            }
            
            Divider()
                .frame(height: modifiers.ht * 1)
                .background(Color.black)

            NavigationLink(destination: Credits()){
                
                HStack{

                    Text("Credits")
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 12))
                }
            }

            
            Divider()
                .frame(height: modifiers.ht * 1)
                .background(Color.black)
            
            
            NavigationLink(destination: Contact()){
                
                HStack{
                    Text("Contact Us")
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 12))
                }
            }
            
            
            Divider()
                .frame(height: modifiers.ht * 1)
                .background(Color.black)
            
            NavigationLink(destination: PrivacyPolicy()){
                
                HStack{
                    Text("Privacy Policy")
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 12))
                }
            }
        }
        .frame(width: modifiers.wt * 70)
        .padding(modifiers.wt * 10)
        .foregroundColor(.black)
        .background(Color.gold2)
        .cornerRadius(modifiers.wt *  10)
        
    }
}








