rot2trans <- function(rot) {
  arr = 0:35;
  for (tp in rot) {
    indices = tp+1;
    for (ii in 1:length(indices)) {
      arr[indices[ii]] = tp[ii %% length(indices) + 1]; 
    }
  }
  arr;
}



transform <-function(vec,vals) {
  return(vec[vals+1]);
} 

transforms = list(
  list(name = "F", rot=list(
    rev(c(9,17,13)),rev(c(10,15,12)),rev(c(14,16,11)),
    rev(c(22,27,0)),rev(c(21,28,1)),rev(c(25,29,2)),rev(c(24,30,3)),rev(c(26,31,4)))),
  list(name = "L", rot=list(
    rev(c(18,26,22)),rev(c(19,24,21)),rev(c(23,25,20)),
    rev(c(35,17,4)),rev(c(33,15,3)),rev(c(32,14,7)),rev(c(28,10,6)),rev(c(27,9,8)))),
  list(name = "R", rot=list(
    rev(c(27,35,31)),rev(c(28,33,30)),rev(c(32,34,29)),
    rev(c(26,8,13)),rev(c(24,6,12)),rev(c(23,5,16)),rev(c(19,1,15)),rev(c(18,0,17)
  ))),
  list(name = "D", rot=list(
    rev(c(0,8,4)),rev(c(1,6,3)),rev(c(5,7,2)),rev(c(31,18,9)),rev(c(30,19,10)),rev(c(34,20,11)),rev(c(33,21,12)),rev(c(35,22,13)
  ))),
  list(name = "u", rot=list(rev(c(26,27,17)))),
  list(name = "b", rot=list(rev(c(18,8,35)))),
  list(name = "l", rot=list(rev(c(9,4,22)))),
  list(name = "r", rot=list(rev(c(0,13,31))))
);

transforms2table <- function(transforms) {
  tble = data.frame();
  for (trs in transforms) {
    tr=rot2trans(trs$rot);rots = lapply(1:nrow(tble), function(x) unlist(tble[x,]));
    names(rots) = row.names(tble);
    trprime=transform(tr,tr);
    subtab=rbind(tr,trprime);
    row.names(subtab) = paste0(trs$name,c("","'"));
    tble=rbind(tble, subtab);
  }
  tble;
}

tble = transforms2table(transforms);

print_transform_table<- function(tble) {
  ss = "new int [][]{";
  for (ii in 1:nrow(tble)) {
    ss=paste0(ss,ifelse(ii==1,"",",")," new int[]  {",paste(tble[ii,],collapse=","),"} //",row.names(tble)[ii],"\n");
  }
  ss = paste0(ss,"}")
  writeLines(ss);
}

print_transform_table(tble);

tip2center = list("b"=19, "r"=1,"u"=28,"l"=10);

trf <- function(pos, trans) { 
  map = unlist(trans)+1; 
  map[pos+1]-1;  
}

rotate<-function(cube,cmd) {
  trans=unlist(tble[cmd,])+1
  cube[trans];
}

multirot<-function(cube, ops) {
  opsarray = strsplit(ops," ")[[1]];
  for (ops in opsarray) {
    if (ops != "") {
      cube = rotate(cube, ops);  
    }
  };
  cube;
}

shuffle_commands=c("D","D'","R","R'","L","L'","l","u","r","b","l'","u'","r'","b'");

shuffle_pyra <- function(pyra, n=20) {
  rots = sample(shuffle_commands, 20, replace=T);
  ops = paste(rots,collapse=" ");
  list(rots=rots, pyra=multirot(pyra, ops));
}

pyranomix <- function(str) {
  s<-strsplit(str,"")[[1]];
 # names(s) <- c(0:35);
  s;
}

colordic=list(d="yellow",f="green",l="red",r="blue" );
