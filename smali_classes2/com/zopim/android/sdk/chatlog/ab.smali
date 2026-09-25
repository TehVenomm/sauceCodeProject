.class final Lcom/zopim/android/sdk/chatlog/ab;
.super Lcom/zopim/android/sdk/chatlog/aa;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/zopim/android/sdk/chatlog/aa<",
        "Lcom/zopim/android/sdk/chatlog/ab;",
        ">;"
    }
.end annotation


# instance fields
.field public a:Ljava/io/File;

.field public b:Ljava/net/URL;

.field public c:I

.field public d:Ljava/lang/String;

.field public e:Z


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/aa;)V
    .locals 0

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/chatlog/aa;-><init>(Lcom/zopim/android/sdk/chatlog/aa;)V

    sget-object p1, Lcom/zopim/android/sdk/chatlog/aa$a;->b:Lcom/zopim/android/sdk/chatlog/aa$a;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    return-void
.end method


# virtual methods
.method public bridge synthetic a(Lcom/zopim/android/sdk/chatlog/aa;)V
    .locals 0

    check-cast p1, Lcom/zopim/android/sdk/chatlog/ab;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/chatlog/ab;->a(Lcom/zopim/android/sdk/chatlog/ab;)V

    return-void
.end method

.method public a(Lcom/zopim/android/sdk/chatlog/ab;)V
    .locals 1

    invoke-super {p0, p1}, Lcom/zopim/android/sdk/chatlog/aa;->a(Lcom/zopim/android/sdk/chatlog/aa;)V

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/ab;->a:Ljava/io/File;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ab;->a:Ljava/io/File;

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/ab;->b:Ljava/net/URL;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ab;->b:Ljava/net/URL;

    iget v0, p1, Lcom/zopim/android/sdk/chatlog/ab;->c:I

    iput v0, p0, Lcom/zopim/android/sdk/chatlog/ab;->c:I

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/ab;->d:Ljava/lang/String;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ab;->d:Ljava/lang/String;

    iget-boolean p1, p1, Lcom/zopim/android/sdk/chatlog/ab;->e:Z

    iput-boolean p1, p0, Lcom/zopim/android/sdk/chatlog/ab;->e:Z

    return-void
.end method

.method public equals(Ljava/lang/Object;)Z
    .locals 4

    const/4 v0, 0x1

    if-ne p0, p1, :cond_0

    return v0

    :cond_0
    instance-of v1, p1, Lcom/zopim/android/sdk/chatlog/ab;

    const/4 v2, 0x0

    if-nez v1, :cond_1

    return v2

    :cond_1
    invoke-super {p0, p1}, Lcom/zopim/android/sdk/chatlog/aa;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_2

    return v2

    :cond_2
    check-cast p1, Lcom/zopim/android/sdk/chatlog/ab;

    iget v1, p0, Lcom/zopim/android/sdk/chatlog/ab;->c:I

    iget v3, p1, Lcom/zopim/android/sdk/chatlog/ab;->c:I

    if-eq v1, v3, :cond_3

    return v2

    :cond_3
    iget-boolean v1, p0, Lcom/zopim/android/sdk/chatlog/ab;->e:Z

    iget-boolean v3, p1, Lcom/zopim/android/sdk/chatlog/ab;->e:Z

    if-eq v1, v3, :cond_4

    return v2

    :cond_4
    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ab;->a:Ljava/io/File;

    if-eqz v1, :cond_5

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ab;->a:Ljava/io/File;

    iget-object v3, p1, Lcom/zopim/android/sdk/chatlog/ab;->a:Ljava/io/File;

    invoke-virtual {v1, v3}, Ljava/io/File;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_6

    goto :goto_0

    :cond_5
    iget-object v1, p1, Lcom/zopim/android/sdk/chatlog/ab;->a:Ljava/io/File;

    if-eqz v1, :cond_6

    :goto_0
    return v2

    :cond_6
    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ab;->b:Ljava/net/URL;

    if-eqz v1, :cond_7

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ab;->b:Ljava/net/URL;

    iget-object v3, p1, Lcom/zopim/android/sdk/chatlog/ab;->b:Ljava/net/URL;

    invoke-virtual {v1, v3}, Ljava/net/URL;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_8

    goto :goto_1

    :cond_7
    iget-object v1, p1, Lcom/zopim/android/sdk/chatlog/ab;->b:Ljava/net/URL;

    if-eqz v1, :cond_8

    :goto_1
    return v2

    :cond_8
    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ab;->d:Ljava/lang/String;

    if-eqz v1, :cond_9

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ab;->d:Ljava/lang/String;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/ab;->d:Ljava/lang/String;

    invoke-virtual {v1, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-nez p1, :cond_b

    goto :goto_2

    :cond_9
    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/ab;->d:Ljava/lang/String;

    if-nez p1, :cond_a

    goto :goto_3

    :cond_a
    :goto_2
    const/4 v0, 0x0

    :cond_b
    :goto_3
    return v0
.end method

.method public hashCode()I
    .locals 3

    invoke-super {p0}, Lcom/zopim/android/sdk/chatlog/aa;->hashCode()I

    move-result v0

    mul-int/lit8 v0, v0, 0x1f

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ab;->a:Ljava/io/File;

    const/4 v2, 0x0

    if-eqz v1, :cond_0

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ab;->a:Ljava/io/File;

    invoke-virtual {v1}, Ljava/io/File;->hashCode()I

    move-result v1

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    add-int/2addr v0, v1

    mul-int/lit8 v0, v0, 0x1f

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ab;->b:Ljava/net/URL;

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ab;->b:Ljava/net/URL;

    invoke-virtual {v1}, Ljava/net/URL;->hashCode()I

    move-result v1

    goto :goto_1

    :cond_1
    const/4 v1, 0x0

    :goto_1
    add-int/2addr v0, v1

    mul-int/lit8 v0, v0, 0x1f

    iget v1, p0, Lcom/zopim/android/sdk/chatlog/ab;->c:I

    add-int/2addr v0, v1

    mul-int/lit8 v0, v0, 0x1f

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ab;->d:Ljava/lang/String;

    if-eqz v1, :cond_2

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ab;->d:Ljava/lang/String;

    invoke-virtual {v1}, Ljava/lang/String;->hashCode()I

    move-result v2

    :cond_2
    add-int/2addr v0, v2

    mul-int/lit8 v0, v0, 0x1f

    iget-boolean v1, p0, Lcom/zopim/android/sdk/chatlog/ab;->e:Z

    add-int/2addr v0, v1

    return v0
.end method

.method public toString()Ljava/lang/String;
    .locals 2

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "file:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ab;->a:Ljava/io/File;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, " uploadUrl:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ab;->b:Ljava/net/URL;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, " progress:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v1, p0, Lcom/zopim/android/sdk/chatlog/ab;->c:I

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, " failed:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v1, p0, Lcom/zopim/android/sdk/chatlog/ab;->e:Z

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    invoke-super {p0}, Lcom/zopim/android/sdk/chatlog/aa;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
