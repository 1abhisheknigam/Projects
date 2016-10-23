 main:
      lda #$0
      sta $2        ! x = 0  (Stack[2])
      lda #$4
      sta $3        !y = 4 (Stack[3]
      lda #$1
      sta $4        !z = 1 (Stack[4])

loop:
      lda $2        !_acc = x
      sub #$13   
      bg rehash   !  if(x>13) break loop
      lda $3	        !_acc = stack[3] = y
     add $2        !_acc += stack[2] = x
     sub $4        !_acc -= stack[4] = z
     sta $3          ! stack[3] = y = y + x + y - z
     sub #$9       !_acc = y still. y - 9
     be rehash    ! y - 9 = 0, go to rehash
     ba out           ! else goto out

rehash:
     lda $2          ! _acc = x
     and $4         !_acc = x && z
     or $3            !_acc = (x && z) || y
     sta $3           ! y = (x && z) || y

out:
    lda $3           !_acc = y.
