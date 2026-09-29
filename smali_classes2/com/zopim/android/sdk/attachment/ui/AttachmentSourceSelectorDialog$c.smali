.class Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$c;
.super Ljava/lang/Object;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = "c"
.end annotation


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;

.field private final b:I

.field private final c:Ljava/lang/String;

.field private final d:Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;ILjava/lang/String;Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$c;->a:Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    iput p2, p0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$c;->b:I

    iput-object p3, p0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$c;->c:Ljava/lang/String;

    iput-object p4, p0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$c;->d:Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

    return-void
.end method


# virtual methods
.method public a()I
    .locals 1

    iget v0, p0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$c;->b:I

    return v0
.end method

.method public b()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$c;->c:Ljava/lang/String;

    return-object v0
.end method

.method public c()Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$c;->d:Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

    return-object v0
.end method
