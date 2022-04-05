//
//  Dasa Prana.swift
//  Tabla App
//
//  Created by Soumya Roychoudhury on 9/9/21.
//

import SwiftUI


struct DasaPrana: View {
    
    @StateObject var modifiers = ScreenModifiers()
    
    @State var showBackground = false
    
    
    var body: some View {
        
        ZStack {

        
            Color.black
                .ignoresSafeArea()

            Image("Ghungroo2").centerCropped()


            VStack{
                
                Spacer()
                    .frame(height: modifiers.ht * 40)
                
                ScrollView{
                    
                    Spacer()
                        .frame(height: modifiers.ht * 30)
                    
                    
                    Group{
                        Text("Dasa Prana")
                            .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 50))
                            .foregroundColor(.gold2)
                            .frame(width: modifiers.wt * 320, height: modifiers.ht * 84 * 0.90)
                        Text("The Ten Vital Elements of Taal")
                            .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 25))
                            .foregroundColor(.gold2)
                            .frame(height: 50)
                            .border(width: modifiers.ht * 2, edges:[.bottom], color: .gold2)
                        
                        
                        Text("Taal is a metrical framework, rhythmic time cycle, consists of specific number of beats. There are 10 vital elements (Dasa Prana) of taal, this is the most fundamental concept of taal.")
                            .multilineTextAlignment(.leading)
                            .font(.system(size: modifiers.wt * 16))
                            .foregroundColor(.gold2)
                            .padding(.vertical, modifiers.ht * 20)
                            .border(width: modifiers.wt * 1, edges: [.bottom], color: .gold2)
                            .padding(.horizontal, modifiers.wt * 20)
                        
                    }
                    
                    Group{
                        
                        Group{Text("1. Kaal").foregroundColor(.red) + Text(" or Time : The time consumed or the duration of an action is termed as Kaal of taal. The entire concept of timing of taal is known as Kaal. It denotes the time to recite whole avartan or just one matra of taal. When we create bandish in taal, Kaal is the time of reciting the whole bandish.")}
                            .multilineTextAlignment(.leading)
                            .font(.system(size: modifiers.wt * 16))
                            .foregroundColor(.gold2)
                            .padding(.vertical, modifiers.ht * 20)
                            .border(width: modifiers.wt * 1, edges: [.bottom], color: .gold2)
                            .padding(.horizontal, modifiers.wt * 20)
                        
                        Group{Text("2. Marga").foregroundColor(.red) + Text(" or Path : Marga refers to path, rhythmic construction of a taal. It says about the arrangements of intervals, number of matras per action, arrangements of taalis, khalis, the path that bandish is taking to reach from first matra to last matra.")}
                            .multilineTextAlignment(.leading)
                            .font(.system(size: modifiers.wt * 16))
                            .foregroundColor(.gold2)
                            .padding(.vertical, modifiers.ht * 20)
                            .border(width: modifiers.wt * 1, edges: [.bottom], color: .gold2)
                            .padding(.horizontal, modifiers.wt * 20)
                    
                        Group{Text("3. Kriya").foregroundColor(.red) + Text(" or Action : Kriya represents an individual hand movements. It is the way of counting time by using beats. So it denotes actions on beats - clapping of palms denotes an accented beat which is known as Sashabda Kriya (such as Taali), hand or finger movements that do not create sound is known as Nishabda Kriya (such as : Khali)")}
                            .multilineTextAlignment(.leading)
                            .font(.system(size: modifiers.wt * 16))
                            .foregroundColor(.gold2)
                            .padding(.vertical, modifiers.ht * 20)
                            .border(width: modifiers.wt * 1, edges: [.bottom], color: .gold2)
                            .padding(.horizontal, modifiers.wt * 20)
                        
                        Group{Text("4. Anga").foregroundColor(.red) + Text(" : The sections of taal is known as Anga. The rhythmic building blocks are called Angas, it is a measuring unit, it is the fundamental block of taal.")}
                            .multilineTextAlignment(.leading)
                            .font(.system(size: modifiers.wt * 16))
                            .foregroundColor(.gold2)
                            .padding(.vertical, modifiers.ht * 20)
                            .border(width: modifiers.wt * 1, edges: [.bottom], color: .gold2)
                            .padding(.horizontal, modifiers.wt * 20)
                        
                        Group{
                            
                            VStack{
                                
                                Group{Text("5. Graha").foregroundColor(.red) + Text(" : The starting point of the badish or composition is known as Graha, there are 2 types of Graha :")}.fixedSize(horizontal: false, vertical: true).multilineTextAlignment(.leading)
                                
                                Spacer()
                                    .frame(height: modifiers.ht * 20)
                                
                                Group{
                                    Text("i. Sam Graha").foregroundColor(.orange) + Text(" : When the composition starts or ends on the Sama.")}.fixedSize(horizontal: false, vertical: true).multilineTextAlignment(.leading)
                                
                                Spacer()
                                    .frame(height: modifiers.ht * 20)
                                
                                Group{
                                    Text("  ii. Visham Graha").foregroundColor(.orange) + Text(" : When the composition does not start or end on the Sama.")}.fixedSize(horizontal: false, vertical: true).multilineTextAlignment(.leading)
                                
                                Group{
                                    Text(" a. Atit").foregroundColor(.yellow) + Text(" : The point after the Sama is said to be Atit.")}.fixedSize(horizontal: false, vertical: true).multilineTextAlignment(.leading)
                                
                                Group{
                                    Text("    b. Anagat").foregroundColor(.yellow) + Text(" : The point prior to the Sama is said to be Anagat.")}.fixedSize(horizontal: false, vertical: true).multilineTextAlignment(.leading)
                                
                                
                            }
                            .font(.system(size: modifiers.wt * 16))
                            .foregroundColor(.gold2)
                            .padding(.vertical, modifiers.ht * 20)
                            .border(width: modifiers.wt * 1, edges: [.bottom], color: .gold2)
                            .padding(.horizontal, modifiers.wt * 20)
                            
                        }
                        
                        Group{
                            
                            VStack{
                                
                                Group{Text("6. Jati").foregroundColor(.red) + Text(" or Groups: Jati of taal depends on the number of bols (grouping of bols) in one beat of taal. There are 5 types of Jati.")}.fixedSize(horizontal: false, vertical: true).multilineTextAlignment(.leading).padding(.horizontal, modifiers.wt * 20)
                                
                                Image("Jati")
                                    .resizable()
                                    .frame(width: modifiers.wt * 300, height: modifiers.wt * 342, alignment: .center)
                            
                                Spacer()
                                    .frame(height: modifiers.ht * 20)
                                
                                   
                            }
                            .font(.system(size: modifiers.wt * 16))
                            .foregroundColor(.gold2)
                            .padding(.vertical, modifiers.ht * 20)
                            .border(width: modifiers.wt * 1, edges: [.bottom], color: .gold2)
                            
                        }
                        
                        Group{Text("7. Kala").foregroundColor(.red) + Text(" or Art : Kala denotes the fractional unit of a Kriya. It says about the way all the bandish is decorated with micro beats of taal.")}
                            .multilineTextAlignment(.leading)
                            .font(.system(size: modifiers.wt * 16))
                            .foregroundColor(.gold2)
                            .padding(.vertical, modifiers.ht * 20)
                            .border(width: modifiers.wt * 1, edges: [.bottom], color: .gold2)
                            .padding(.horizontal, modifiers.wt * 20)
                        
                        Group{Text("8. Laya").foregroundColor(.red) + Text(" or Tempo : Laya means speed or tempo. It indicates the spaces in between the beats.There are 3 types of Laya : Vilambit, Madhya, and Drut.")}
                            .multilineTextAlignment(.leading)
                            .font(.system(size: modifiers.wt * 16))
                            .foregroundColor(.gold2)
                            .padding(.vertical, modifiers.ht * 20)
                            .border(width: modifiers.wt * 1, edges: [.bottom], color: .gold2)
                            .padding(.horizontal, modifiers.wt * 20)
                        
                        Group{
                            
                            VStack{
                                
                                Group{Text("9. Yati").foregroundColor(.red) + Text(" or Shape : Yati is the shape of rhythmic phrases, it refers to a particular pattern of bols or syllables by arranging the pauses in various ways. The main importance is given to beatification of the rhythm. This is termed as Yati. In the whole journey of composition, we can mix different length of bols, different speed of rhythm. This effort of changing the rhythm in slow, normal and fast with different size of bols is termed as Yati. There are 5 types of Yati :")}.fixedSize(horizontal: false, vertical: true).multilineTextAlignment(.leading)
                                
                                Group{
                                    Spacer()
                                        .frame(height: modifiers.ht * 20)
                                    
                                    Group{
                                        Text("i. Samayati").foregroundColor(.orange) + Text(": The density of flow is uniform, each line of this formation is equal.")}.fixedSize(horizontal: false, vertical: true).multilineTextAlignment(.leading)
                                    
                                    Image("Samayati")
                                        .resizable()
                                        .frame(width: modifiers.wt * 130, height: modifiers.wt * 130, alignment: .center)
                                    

                                }
                                
                                Group{
                                    Spacer()
                                        .frame(height: modifiers.ht * 20)
                                    
                                    Group{
                                        Text("ii. Strotgata Yati").foregroundColor(.orange) + Text(": When Compositions are shaped to expand gradually while approaching the end.")}.fixedSize(horizontal: false, vertical: true).multilineTextAlignment(.leading)
                                    
                                    Image("Strotgata")
                                        .resizable()
                                        .frame(width: modifiers.wt * 200, height: modifiers.wt * 120, alignment: .center)
                                    

                                }
                                
                                Group{
                                    Spacer()
                                        .frame(height: modifiers.ht * 20)
                                    
                                    Group{
                                        Text("iii. Mridanga").foregroundColor(.orange) + Text(": When Compositions are shaped to expand gradually to a point and then converge while approaching the end. It resembles the shape of a mridanga with narrow ends and a wide center.")}.fixedSize(horizontal: false, vertical: true).multilineTextAlignment(.leading)
                                    
                                    Image("Mridanga")
                                        .resizable()
                                        .frame(width: modifiers.wt * 200, height: modifiers.wt * 200, alignment: .center)
                                    

                                }
                                
                                Group{
                                    Spacer()
                                        .frame(height: modifiers.ht * 20)
                                    
                                    Group{
                                        Text("iv. Damru").foregroundColor(.orange) + Text(": The opposite of mridanga yati. Compositions are shaped to converge gradually to a point and then expand while approaching the end. It resembles a damru with wide ends and a narrow center.")}.fixedSize(horizontal: false, vertical: true).multilineTextAlignment(.leading)
                                    
                                    Image("Damru")
                                        .resizable()
                                        .frame(width: modifiers.wt * 200, height: modifiers.wt * 200, alignment: .center)
                                    

                                }
                                
                                Group{
                                    Spacer()
                                        .frame(height: modifiers.ht * 20)
                                    
                                    Group{
                                        Text("v. Pipilika").foregroundColor(.orange) + Text(": When Compositions are shaped to converge gradually to a point and then gradually expand while approaching the end.")}.fixedSize(horizontal: false, vertical: true).multilineTextAlignment(.leading)
                                    
                                    Image("Piplika 1")
                                        .resizable()
                                        .frame(width: modifiers.wt * 200, height: modifiers.wt * 200, alignment: .center)
                                    

                                }
                                
                                Group{
                                    Spacer()
                                        .frame(height: modifiers.ht * 20)
                                    
                                    Group{
                                        Text("vi. Gopucha").foregroundColor(.orange) + Text(": When compositions are shaped to converge gradually while approaching the end.")}.fixedSize(horizontal: false, vertical: true).multilineTextAlignment(.leading)
                                    
                                    Image("Gopucha")
                                        .resizable()
                                        .frame(width: modifiers.wt * 200, height: modifiers.wt * 120, alignment: .center)
                                    

                                }
                                   
                            }
                            .font(.system(size: modifiers.wt * 16))
                            .foregroundColor(.gold2)
                            .padding(.vertical, modifiers.ht * 20)
                            .border(width: modifiers.wt * 1, edges: [.bottom], color: .gold2)
                            .padding(.horizontal, modifiers.wt * 20)
                        }
                        
                        Group{Text("10. Prastaar").foregroundColor(.red) + Text(" : The process of development of compositions in taal. It refers to different variations of compositions. It is performed on the basis of permutations, it is the Bistar of compositions, lari or Baant of compositions.")}
                            .multilineTextAlignment(.leading)
                            .font(.system(size: modifiers.wt * 16))
                            .foregroundColor(.gold2)
                            .padding(.vertical, modifiers.ht * 20)
                            .border(width: modifiers.wt * 1, edges: [.bottom], color: .gold2)
                            .padding(.horizontal, modifiers.wt * 20)
                    }
                    
                    Spacer()
                    
                }
                .frame(width: modifiers.wt * 380, height: modifiers.ht * 844 * 0.80)
                .background(Color.black)
                .cornerRadius(modifiers.wt * 40.0)
                
            }
            
 
 
            
            
    
        }
 

        
        
        
    }
}


struct DasaPrana_Previews: PreviewProvider {
    static var previews: some View {
        DasaPrana()
    }
}
