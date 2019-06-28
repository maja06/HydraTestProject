import { HydraTestProjectTemplatePage } from './app.po';

describe('HydraTestProject App', function() {
  let page: HydraTestProjectTemplatePage;

  beforeEach(() => {
    page = new HydraTestProjectTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
