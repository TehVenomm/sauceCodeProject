.class Lcom/helpshift/common/platform/AndroidFAQSearchDM$1$1;
.super Landroid/os/Handler;
.source "AndroidFAQSearchDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/common/platform/AndroidFAQSearchDM$1;->run()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lcom/helpshift/common/platform/AndroidFAQSearchDM$1;


# direct methods
.method constructor <init>(Lcom/helpshift/common/platform/AndroidFAQSearchDM$1;)V
    .locals 0

    .line 36
    iput-object p1, p0, Lcom/helpshift/common/platform/AndroidFAQSearchDM$1$1;->this$1:Lcom/helpshift/common/platform/AndroidFAQSearchDM$1;

    invoke-direct {p0}, Landroid/os/Handler;-><init>()V

    return-void
.end method


# virtual methods
.method public handleMessage(Landroid/os/Message;)V
    .locals 1

    .line 39
    iget p1, p1, Landroid/os/Message;->what:I

    sget v0, Lcom/helpshift/support/constants/GetSectionsCallBackStatus;->API_SUCCESS_NO_NEW_DATA:I

    if-eq p1, v0, :cond_0

    .line 40
    iget-object p1, p0, Lcom/helpshift/common/platform/AndroidFAQSearchDM$1$1;->this$1:Lcom/helpshift/common/platform/AndroidFAQSearchDM$1;

    iget-object p1, p1, Lcom/helpshift/common/platform/AndroidFAQSearchDM$1;->this$0:Lcom/helpshift/common/platform/AndroidFAQSearchDM;

    iget-object p1, p1, Lcom/helpshift/common/platform/AndroidFAQSearchDM;->data:Lcom/helpshift/support/HSApiData;

    invoke-virtual {p1}, Lcom/helpshift/support/HSApiData;->loadIndex()V

    .line 41
    invoke-static {}, Lcom/helpshift/support/util/HSTransliterator;->init()V

    :cond_0
    return-void
.end method
