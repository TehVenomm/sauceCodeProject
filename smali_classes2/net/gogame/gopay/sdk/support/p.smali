.class final Lnet/gogame/gopay/sdk/support/p;
.super Landroid/os/AsyncTask;


# instance fields
.field final synthetic a:Ljava/lang/String;

.field final synthetic b:Ljava/lang/String;

.field final synthetic c:Ljava/lang/String;

.field final synthetic d:Lnet/gogame/gopay/sdk/support/q;


# direct methods
.method constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lnet/gogame/gopay/sdk/support/q;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/support/p;->a:Ljava/lang/String;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/support/p;->b:Ljava/lang/String;

    iput-object p3, p0, Lnet/gogame/gopay/sdk/support/p;->c:Ljava/lang/String;

    iput-object p4, p0, Lnet/gogame/gopay/sdk/support/p;->d:Lnet/gogame/gopay/sdk/support/q;

    invoke-direct {p0}, Landroid/os/AsyncTask;-><init>()V

    return-void
.end method


# virtual methods
.method protected final synthetic doInBackground([Ljava/lang/Object;)Ljava/lang/Object;
    .locals 2

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/p;->isCancelled()Z

    move-result p1

    const/4 v0, 0x0

    if-eqz p1, :cond_0

    return-object v0

    :cond_0
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-static {}, Lnet/gogame/gopay/sdk/support/m;->o()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "/assets/"

    invoke-virtual {p1, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lnet/gogame/gopay/sdk/support/p;->a:Ljava/lang/String;

    invoke-virtual {p1, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    new-instance v1, Ljava/io/File;

    invoke-direct {v1, p1}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    invoke-virtual {v1}, Ljava/io/File;->exists()Z

    move-result v1

    if-eqz v1, :cond_1

    new-instance v0, Landroid/graphics/BitmapFactory$Options;

    invoke-direct {v0}, Landroid/graphics/BitmapFactory$Options;-><init>()V

    invoke-static {p1, v0}, Landroid/graphics/BitmapFactory;->decodeFile(Ljava/lang/String;Landroid/graphics/BitmapFactory$Options;)Landroid/graphics/Bitmap;

    move-result-object v0

    :cond_1
    if-nez v0, :cond_2

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/p;->b:Ljava/lang/String;

    if-eqz p1, :cond_2

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/p;->b:Ljava/lang/String;

    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result p1

    if-lez p1, :cond_2

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/p;->b:Ljava/lang/String;

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/p;->c:Ljava/lang/String;

    invoke-static {p1, v0}, Lnet/gogame/gopay/sdk/support/m;->a(Ljava/lang/String;Ljava/lang/String;)Landroid/graphics/Bitmap;

    move-result-object v0

    :cond_2
    if-eqz v0, :cond_3

    invoke-static {}, Lnet/gogame/gopay/sdk/support/m;->p()Landroid/util/LruCache;

    move-result-object p1

    iget-object v1, p0, Lnet/gogame/gopay/sdk/support/p;->a:Ljava/lang/String;

    invoke-virtual {p1, v1, v0}, Landroid/util/LruCache;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_3
    return-object v0
.end method

.method protected final synthetic onPostExecute(Ljava/lang/Object;)V
    .locals 1

    check-cast p1, Landroid/graphics/Bitmap;

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/p;->isCancelled()Z

    move-result v0

    if-nez v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/p;->d:Lnet/gogame/gopay/sdk/support/q;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/p;->d:Lnet/gogame/gopay/sdk/support/q;

    invoke-interface {v0, p1}, Lnet/gogame/gopay/sdk/support/q;->a(Landroid/graphics/Bitmap;)V

    :cond_0
    return-void
.end method
