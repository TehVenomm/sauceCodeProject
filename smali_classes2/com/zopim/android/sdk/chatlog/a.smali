.class final Lcom/zopim/android/sdk/chatlog/a;
.super Lcom/zopim/android/sdk/chatlog/aa;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/zopim/android/sdk/chatlog/aa<",
        "Lcom/zopim/android/sdk/chatlog/a;",
        ">;"
    }
.end annotation


# instance fields
.field public a:Ljava/net/URL;

.field public b:Ljava/lang/Long;

.field public c:Ljava/lang/String;

.field public d:Ljava/io/File;

.field public e:Ljava/lang/String;

.field public f:[Ljava/lang/String;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/aa;)V
    .locals 0

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/chatlog/aa;-><init>(Lcom/zopim/android/sdk/chatlog/aa;)V

    const/4 p1, 0x0

    new-array p1, p1, [Ljava/lang/String;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    sget-object p1, Lcom/zopim/android/sdk/chatlog/aa$a;->c:Lcom/zopim/android/sdk/chatlog/aa$a;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    return-void
.end method


# virtual methods
.method public a(Lcom/zopim/android/sdk/chatlog/a;)V
    .locals 1

    invoke-super {p0, p1}, Lcom/zopim/android/sdk/chatlog/aa;->a(Lcom/zopim/android/sdk/chatlog/aa;)V

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/a;->a:Ljava/net/URL;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/a;->a:Ljava/net/URL;

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/a;->b:Ljava/lang/Long;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/a;->b:Ljava/lang/Long;

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/a;->c:Ljava/lang/String;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/a;->c:Ljava/lang/String;

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/a;->e:Ljava/lang/String;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/a;->e:Ljava/lang/String;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    return-void
.end method

.method public bridge synthetic a(Lcom/zopim/android/sdk/chatlog/aa;)V
    .locals 0

    check-cast p1, Lcom/zopim/android/sdk/chatlog/a;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/chatlog/a;->a(Lcom/zopim/android/sdk/chatlog/a;)V

    return-void
.end method

.method public equals(Ljava/lang/Object;)Z
    .locals 3

    if-ne p0, p1, :cond_0

    const/4 p1, 0x1

    return p1

    :cond_0
    const/4 v0, 0x0

    if-eqz p1, :cond_d

    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v1

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v2

    if-eq v1, v2, :cond_1

    goto/16 :goto_5

    :cond_1
    invoke-super {p0, p1}, Lcom/zopim/android/sdk/chatlog/aa;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_2

    return v0

    :cond_2
    check-cast p1, Lcom/zopim/android/sdk/chatlog/a;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->a:Ljava/net/URL;

    if-eqz v1, :cond_3

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->a:Ljava/net/URL;

    iget-object v2, p1, Lcom/zopim/android/sdk/chatlog/a;->a:Ljava/net/URL;

    invoke-virtual {v1, v2}, Ljava/net/URL;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_4

    goto :goto_0

    :cond_3
    iget-object v1, p1, Lcom/zopim/android/sdk/chatlog/a;->a:Ljava/net/URL;

    if-eqz v1, :cond_4

    :goto_0
    return v0

    :cond_4
    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->b:Ljava/lang/Long;

    if-eqz v1, :cond_5

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->b:Ljava/lang/Long;

    iget-object v2, p1, Lcom/zopim/android/sdk/chatlog/a;->b:Ljava/lang/Long;

    invoke-virtual {v1, v2}, Ljava/lang/Long;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_6

    goto :goto_1

    :cond_5
    iget-object v1, p1, Lcom/zopim/android/sdk/chatlog/a;->b:Ljava/lang/Long;

    if-eqz v1, :cond_6

    :goto_1
    return v0

    :cond_6
    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->c:Ljava/lang/String;

    if-eqz v1, :cond_7

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->c:Ljava/lang/String;

    iget-object v2, p1, Lcom/zopim/android/sdk/chatlog/a;->c:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_8

    goto :goto_2

    :cond_7
    iget-object v1, p1, Lcom/zopim/android/sdk/chatlog/a;->c:Ljava/lang/String;

    if-eqz v1, :cond_8

    :goto_2
    return v0

    :cond_8
    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->d:Ljava/io/File;

    if-eqz v1, :cond_9

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->d:Ljava/io/File;

    iget-object v2, p1, Lcom/zopim/android/sdk/chatlog/a;->d:Ljava/io/File;

    invoke-virtual {v1, v2}, Ljava/io/File;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_a

    goto :goto_3

    :cond_9
    iget-object v1, p1, Lcom/zopim/android/sdk/chatlog/a;->d:Ljava/io/File;

    if-eqz v1, :cond_a

    :goto_3
    return v0

    :cond_a
    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->e:Ljava/lang/String;

    if-eqz v1, :cond_b

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->e:Ljava/lang/String;

    iget-object v2, p1, Lcom/zopim/android/sdk/chatlog/a;->e:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_c

    goto :goto_4

    :cond_b
    iget-object v1, p1, Lcom/zopim/android/sdk/chatlog/a;->e:Ljava/lang/String;

    if-eqz v1, :cond_c

    :goto_4
    return v0

    :cond_c
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    invoke-static {v0, p1}, Ljava/util/Arrays;->equals([Ljava/lang/Object;[Ljava/lang/Object;)Z

    move-result p1

    return p1

    :cond_d
    :goto_5
    return v0
.end method

.method public hashCode()I
    .locals 3

    invoke-super {p0}, Lcom/zopim/android/sdk/chatlog/aa;->hashCode()I

    move-result v0

    mul-int/lit8 v0, v0, 0x1f

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->a:Ljava/net/URL;

    const/4 v2, 0x0

    if-eqz v1, :cond_0

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->a:Ljava/net/URL;

    invoke-virtual {v1}, Ljava/net/URL;->hashCode()I

    move-result v1

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    add-int/2addr v0, v1

    mul-int/lit8 v0, v0, 0x1f

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->b:Ljava/lang/Long;

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->b:Ljava/lang/Long;

    invoke-virtual {v1}, Ljava/lang/Long;->hashCode()I

    move-result v1

    goto :goto_1

    :cond_1
    const/4 v1, 0x0

    :goto_1
    add-int/2addr v0, v1

    mul-int/lit8 v0, v0, 0x1f

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->c:Ljava/lang/String;

    if-eqz v1, :cond_2

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->c:Ljava/lang/String;

    invoke-virtual {v1}, Ljava/lang/String;->hashCode()I

    move-result v1

    goto :goto_2

    :cond_2
    const/4 v1, 0x0

    :goto_2
    add-int/2addr v0, v1

    mul-int/lit8 v0, v0, 0x1f

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->d:Ljava/io/File;

    if-eqz v1, :cond_3

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->d:Ljava/io/File;

    invoke-virtual {v1}, Ljava/io/File;->hashCode()I

    move-result v1

    goto :goto_3

    :cond_3
    const/4 v1, 0x0

    :goto_3
    add-int/2addr v0, v1

    mul-int/lit8 v0, v0, 0x1f

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->e:Ljava/lang/String;

    if-eqz v1, :cond_4

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->e:Ljava/lang/String;

    invoke-virtual {v1}, Ljava/lang/String;->hashCode()I

    move-result v1

    goto :goto_4

    :cond_4
    const/4 v1, 0x0

    :goto_4
    add-int/2addr v0, v1

    mul-int/lit8 v0, v0, 0x1f

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    if-eqz v1, :cond_5

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    invoke-static {v1}, Ljava/util/Arrays;->hashCode([Ljava/lang/Object;)I

    move-result v2

    :cond_5
    add-int/2addr v0, v2

    return v0
.end method

.method public toString()Ljava/lang/String;
    .locals 2

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "options:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, " attachFile:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->d:Ljava/io/File;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, " attachUrl:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->a:Ljava/net/URL;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, " attachName:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->c:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " attachSize:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/a;->b:Ljava/lang/Long;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-super {p0}, Lcom/zopim/android/sdk/chatlog/aa;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
